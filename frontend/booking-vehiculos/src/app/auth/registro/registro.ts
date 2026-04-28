import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

const PASSWORD_RULES = [
  { id: 'len', label: 'Mínimo 8 caracteres', test: (p: string) => p.length >= 8 },
  { id: 'upper', label: 'Una mayúscula', test: (p: string) => /[A-Z]/.test(p) },
  { id: 'lower', label: 'Una minúscula', test: (p: string) => /[a-z]/.test(p) },
  { id: 'number', label: 'Un número', test: (p: string) => /[0-9]/.test(p) },
  { id: 'symbol', label: 'Un símbolo (!@#$%…)', test: (p: string) => /[!@#$%^&*()\-_=+.,?]/.test(p) },
];

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './registro.html',
  styleUrls: ['./registro.scss']
})
export class RegistroComponent {
  private fb = inject(FormBuilder);
  private http = inject(HttpClient);
  private router = inject(Router);

  private readonly base = `${environment.apiUrl}/api/v1`;

  // Password Strength Signals
  private _pw = signal('');
  rules = computed(() => PASSWORD_RULES.map(r => ({ ...r, ok: r.test(this._pw()) })));

  // Regex para validación del Form
  private passwordRegex = '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&.])[A-Za-z\\d@$!%*?&.]{8,}$';

  registroForm = this.fb.group({
    identificacion: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
    nombres: ['', [Validators.required, Validators.minLength(2), Validators.pattern('^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$')]],
    apellidos: ['', [Validators.required, Validators.minLength(2), Validators.pattern('^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$')]],
    correo: ['', [Validators.required, Validators.email]],
    telefono: ['', [Validators.required, Validators.pattern('^09[0-9]{8}$')]],
    password: ['', [Validators.required, Validators.pattern(this.passwordRegex)]]
  });

  isLoading = false;
  globalError = '';
  showPassword = false;

  get f() { return this.registroForm.controls; }

  constructor() {
    // Sincronizar el signal de fuerza con el valor del campo password
    this.registroForm.get('password')?.valueChanges.subscribe(val => this._pw.set(val || ''));
  }

  hasError(field: string) {
    const c = this.registroForm.get(field);
    return c?.invalid && (c?.dirty || c?.touched);
  }

  isOk(field: string) {
    const c = this.registroForm.get(field);
    return c?.valid && (c?.dirty || c?.touched);
  }

  registrar() {
    if (this.registroForm.invalid) {
      this.registroForm.markAllAsTouched();
      this.globalError = 'Por favor, cumple con todos los requisitos de seguridad.';
      return;
    }

    this.isLoading = true;
    const formVal = this.registroForm.value;

    const payload = {
      CLI_cedula: formVal.identificacion!.trim(),
      CLI_nombres: formVal.nombres!.trim(),
      CLI_apellidos: formVal.apellidos!.trim(),
      CLI_telefono: formVal.telefono!.trim(),
      CLI_correo: formVal.correo!.trim().toLowerCase(),
      Password: formVal.password!,
      CLI_usuarioCreacion: formVal.correo!.trim()
    };

    this.http.post<any>(`${this.base}/clientes`, payload).subscribe({
      next: () => {
        this.isLoading = false;
        alert('✅ ¡Cuenta creada! Ahora puedes iniciar sesión.');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.isLoading = false;
        this.globalError = err.error?.Message || err.error?.message || 'Error al registrar. Inténtalo de nuevo.';
      }
    });
  }

  togglePassword() { this.showPassword = !this.showPassword; }
  irALogin() { this.router.navigate(['/login']); }
}
