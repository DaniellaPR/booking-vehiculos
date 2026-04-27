import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, tap } from 'rxjs';

// Clave del claim de rol que usa .NET (ClaimTypes.Role)
const ROL_CLAIM = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);

  // ── Lectura de sesión ──────────────────────────────────────────────────────

  getToken(): string | null {
    return localStorage.getItem('drivex_token') ?? sessionStorage.getItem('drivex_token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getName(): string {
    return localStorage.getItem('drivex_nombre') ?? sessionStorage.getItem('drivex_nombre') ?? 'Usuario';
  }

  getEmail(): string {
    return localStorage.getItem('drivex_email') ?? sessionStorage.getItem('drivex_email') ?? '';
  }

  getRoles(): string[] {
    const raw = localStorage.getItem('drivex_roles') ?? sessionStorage.getItem('drivex_roles');
    return raw ? JSON.parse(raw) : [];
  }

  isAdmin(): boolean    { return this.getRoles().includes('ADMIN'); }
  isVendedor(): boolean { return this.getRoles().includes('VENDEDOR'); }
  isCliente(): boolean  {
    const roles = this.getRoles();
    return roles.includes('CLIENTE') || (!this.isAdmin() && !this.isVendedor());
  }

  // ── Login ─────────────────────────────────────────────────────────────────
  // El backend devuelve: { Success, Message, Data: { Token, ExpirationUtc } }
  // El campo del body es "Correo" (no correoElectronico)

  login(correo: string, password: string, remember: boolean = true): Observable<any> {
    return this.http
      .post<any>(`${environment.apiUrl}/auth/login`, { Correo: correo, Password: password })
      .pipe(
        tap((res: any) => {
          // El token viene en res.Data.Token (PascalCase porque el back tiene PropertyNamingPolicy = null)
          const token: string | undefined = res?.Data?.Token ?? res?.data?.token ?? res?.token;
          if (!token) {
            console.error('No se recibió token. Respuesta completa:', res);
            return;
          }
          this._saveSession(token, remember);
        })
      );
  }

  // ── Registro de cliente nuevo ─────────────────────────────────────────────
  // POST /clientes — crea el registro en la tabla Cliente
  // Nota: en el back no hay hash de contraseña para clientes aún (Reto 1)
  registrarCliente(data: {
    CLI_nombres: string;
    CLI_apellidos: string;
    CLI_cedula: string;
    CLI_telefono?: string;
  }): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/clientes`, data);
  }

  // ── Logout ────────────────────────────────────────────────────────────────

  logout(): void {
    ['drivex_token', 'drivex_roles', 'drivex_email', 'drivex_nombre'].forEach(k => {
      localStorage.removeItem(k);
      sessionStorage.removeItem(k);
    });
  }

  // ── Privado ───────────────────────────────────────────────────────────────

  private _saveSession(token: string, remember: boolean): void {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));

      // El rol viene como ClaimTypes.Role (.NET) — puede ser string o array
      const rolRaw = payload[ROL_CLAIM];
      const roles: string[] = Array.isArray(rolRaw) ? rolRaw : (rolRaw ? [rolRaw] : ['CLIENTE']);

      const email:  string = payload['email']  ?? payload[ROL_CLAIM] ?? '';
      const nombre: string = payload['name']   ?? payload['unique_name'] ?? email;

      const store = remember ? localStorage : sessionStorage;
      store.setItem('drivex_token',  token);
      store.setItem('drivex_roles',  JSON.stringify(roles));
      store.setItem('drivex_email',  email);
      store.setItem('drivex_nombre', nombre);
    } catch (e) {
      console.error('Error al decodificar JWT:', e);
    }
  }
}
