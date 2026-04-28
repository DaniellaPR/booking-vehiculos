import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';
// IMPORTANTE: Importamos HttpClient y environment para hacer la petición directa
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';


// ── Patrones de seguridad ────────────────────────────────────────────────────
const DANGEROUS = [
  /[<>'"`;\\]/,
  /(\b(SELECT|INSERT|UPDATE|DELETE|DROP|UNION|EXEC)\b)/i,
  /(--)|(\/\*)/,
  /(javascript:|data:)/i,
  /[\x00-\x08\x0E-\x1F\x7F]/,
];

const PASSWORD_RULES = [
  { id: 'len', label: 'Mínimo 8 caracteres', test: (p: string) => p.length >= 8 },
  { id: 'upper', label: 'Una mayúscula', test: (p: string) => /[A-Z]/.test(p) },
  { id: 'lower', label: 'Una minúscula', test: (p: string) => /[a-z]/.test(p) },
  { id: 'number', label: 'Un número', test: (p: string) => /[0-9]/.test(p) },
  { id: 'symbol', label: 'Un símbolo (!@#$%…)', test: (p: string) => /[!@#$%^&*()\-_=+.,?]/.test(p) },
];

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrls: ['./login.scss'],
})
export class LoginComponent {
  // Inyecciones
  private auth = inject(AuthService);

  private http = inject(HttpClient);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  // Modelos del formulario
  email = '';
  password = '';
  remember = false;
  showPassword = false;

  // Estados UI (Signals)
  isLoading = signal(false);
  showRedirect = signal(false);
  redirectRole = signal('');
  emailError = signal<string | null>(null);
  passwordError = signal<string | null>(null);
  globalError = signal<string | null>(null);

  emailTouched = false;
  passwordTouched = false;

  // Calculadoras de fuerza de contraseña
  private _pw = signal('');
  rules = computed(() => PASSWORD_RULES.map(r => ({ ...r, ok: r.test(this._pw()) })));
  strength = computed(() => Math.round(this.rules().filter(r => r.ok).length / PASSWORD_RULES.length * 100));
  strengthClass = computed(() => {
    const s = this.strength();
    if (s <= 40) return 'weak'; if (s <= 70) return 'fair'; if (s <= 90) return 'good'; return 'strong';
  });
  strengthLabel = computed(() => {
    const s = this.strength();
    if (s === 0) return ''; if (s <= 40) return 'Débil'; if (s <= 70) return 'Moderada';
    if (s <= 90) return 'Buena'; return 'Fuerte';
  });

  // Control de intentos y bloqueo
  private fails = 0;
  private lockedUntil: number | null = null;
  lockSecs = signal(0);
  private lockTimer?: ReturnType<typeof setInterval>;

  // Validaciones
  private isDangerous = (v: string) => DANGEROUS.some(p => p.test(v));

  private validateEmail(v: string): string | null {
    if (!v.trim()) return 'El correo es obligatorio.';
    if (v.length > 254) return 'Máximo 254 caracteres.';
    if (this.isDangerous(v)) return 'Caracteres no permitidos.';
    if (!/^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$/.test(v.trim())) return 'Correo inválido.';
    return null;
  }

  private validatePassword(v: string): string | null {
    // En el LOGIN solo validamos que no esté vacía y no tenga caracteres peligrosos.
    // Las reglas de complejidad se aplican solo en el REGISTRO para no bloquear
    // a usuarios cuya contraseña fue creada con criterios diferentes.
    if (!v) return 'La contraseña es obligatoria.';
    if (v.length > 128) return 'Máximo 128 caracteres.';
    if (this.isDangerous(v)) return 'Caracteres no permitidos.';
    return null;
  }

  // Eventos de Input
  onEmailInput(e: Event) {
    this.email = (e.target as HTMLInputElement).value.replace(/\s/g, '');
    if (this.emailTouched) this.emailError.set(this.validateEmail(this.email));
  }
  onEmailBlur() { this.emailTouched = true; this.emailError.set(this.validateEmail(this.email)); }

  onPasswordInput(e: Event) {
    this.password = (e.target as HTMLInputElement).value;
    this._pw.set(this.password);
    if (this.passwordTouched) this.passwordError.set(this.validatePassword(this.password));
  }
  onPasswordBlur() { this.passwordTouched = true; this.passwordError.set(this.validatePassword(this.password)); }

  togglePassword() { this.showPassword = !this.showPassword; }

  private startLock() {
    if (this.lockTimer) clearInterval(this.lockTimer);
    this.lockTimer = setInterval(() => {
      const r = Math.ceil(((this.lockedUntil ?? 0) - Date.now()) / 1000);
      if (r <= 0) {
        clearInterval(this.lockTimer);
        this.lockSecs.set(0);
        this.globalError.set(null);
        this.lockedUntil = null;
      }
      else {
        this.lockSecs.set(r);
        this.globalError.set(`Bloqueado temporalmente. Espera ${r}s antes de reintentar.`);
      }
    }, 1000);
  }

  // Ejecución del Login
  handleLogin() {
    this.emailTouched = this.passwordTouched = true;
    this.globalError.set(null);

    if (this.lockedUntil && Date.now() < this.lockedUntil) {
      this.startLock();
      return;
    }

    const ee = this.validateEmail(this.email);
    this.emailError.set(ee);
    if (ee) return;

    const pe = this.validatePassword(this.password);
    this.passwordError.set(pe);
    if (pe) return;

    this.isLoading.set(true);

    // ✅ Limpiar sesión previa antes de intentar login.
    // Esto evita que el interceptor adjunte un token expirado al request.
    this.auth.logout();

    // ✅ Usar AuthService directamente — él guarda el token con _saveSession
    this.auth.login(this.email.trim().toLowerCase(), this.password, this.remember).subscribe({
      next: (res) => {
        this.isLoading.set(false);
        this.fails = 0;

        // ✅ Leer roles desde el AuthService (ya están normalizados a ADMINISTRADOR/VENDEDOR)
        const roles = this.auth.getRoles();

        if (roles.includes('ADMINISTRADOR') || roles.includes('VENDEDOR')) {
          this.redirectRole.set('ADMINISTRADOR');
          this.showRedirect.set(true);
          setTimeout(() => this.router.navigate(['/admin/dashboard']), 1000);
        } else {
          this.redirectRole.set('CLIENTE');
          this.showRedirect.set(true);
          setTimeout(() => {
            const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
            this.router.navigateByUrl(returnUrl || '/');
          }, 1000);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.fails++;

        if (this.fails >= 5) {
          this.lockedUntil = Date.now() + Math.min(30_000 * Math.pow(2, this.fails - 5), 300_000);
          this.startLock();
        } else {
          const msg = err.error?.Message || err.error?.message || 'Correo o contraseña incorrectos.';
          this.globalError.set(msg);
        }
      }
    });
  }

  irARegistro() { this.router.navigate(['/registro']); }
  irAlMarketplace() { this.router.navigate(['/']); }
}
