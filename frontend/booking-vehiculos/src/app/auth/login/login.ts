import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

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
  private auth = inject(AuthService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  email = '';
  password = '';
  remember = false;
  showPassword = false;

  isLoading = signal(false);
  showRedirect = signal(false);
  redirectRole = signal('');
  emailError = signal<string | null>(null);
  passwordError = signal<string | null>(null);
  globalError = signal<string | null>(null);

  emailTouched = false;
  passwordTouched = false;

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

  // Rate limit
  private fails = 0;
  private lockedUntil: number | null = null;
  lockSecs = signal(0);
  private lockTimer?: ReturnType<typeof setInterval>;

  private isDangerous = (v: string) => DANGEROUS.some(p => p.test(v));

  private validateEmail(v: string): string | null {
    if (!v.trim()) return 'El correo es obligatorio.';
    if (v.length > 254) return 'Máximo 254 caracteres.';
    if (this.isDangerous(v)) return 'Caracteres no permitidos.';
    if (!/^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$/.test(v.trim())) return 'Correo inválido.';
    return null;
  }

  private validatePassword(v: string): string | null {
    if (!v) return 'La contraseña es obligatoria.';
    if (v.length > 128) return 'Máximo 128 caracteres.';
    if (this.isDangerous(v)) return 'Caracteres no permitidos.';
    const fail = PASSWORD_RULES.find(r => !r.test(v));
    if (fail) return `Requerido: ${fail.label}`;
    return null;
  }

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
      if (r <= 0) { clearInterval(this.lockTimer); this.lockSecs.set(0); this.globalError.set(null); this.lockedUntil = null; }
      else { this.lockSecs.set(r); this.globalError.set(`Bloqueado. Espera ${r}s antes de reintentar.`); }
    }, 1000);
  }

  handleLogin() {
    this.emailTouched = this.passwordTouched = true;
    this.globalError.set(null);

    if (this.lockedUntil && Date.now() < this.lockedUntil) { this.startLock(); return; }

    const ee = this.validateEmail(this.email);
    this.emailError.set(ee);
    if (ee) return;

    const pe = this.validatePassword(this.password);
    this.passwordError.set(pe);
    if (pe) return;

    this.isLoading.set(true);

    this.auth.login(this.email.trim().toLowerCase(), this.password, this.remember).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.fails = 0;

        if (this.auth.isAdmin()) this.redirectRole.set('ADMINISTRADOR');
        else if (this.auth.isVendedor()) this.redirectRole.set('VENDEDOR');
        else this.redirectRole.set('CLIENTE');
        this.showRedirect.set(true);

        setTimeout(() => {
          // Vuelve a la URL que intentaba visitar (ej: /reservar/xxx)
          const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
          if (returnUrl) { this.router.navigateByUrl(returnUrl); return; }
          if (this.auth.isAdmin() || this.auth.isVendedor()) this.router.navigate(['/admin/dashboard']);
          else this.router.navigate(['/']);
        }, 800);
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.fails++;
        if (this.fails >= 5) {
          this.lockedUntil = Date.now() + Math.min(30_000 * Math.pow(2, this.fails - 5), 300_000);
          this.startLock();
        } else {
          this.globalError.set(err.error?.Message ?? err.error?.message ?? 'Credenciales incorrectas.');
        }
      }
    });
  }

  irARegistro() { this.router.navigate(['/registro']); }
  irAlMarketplace() { this.router.navigate(['/']); }
}
