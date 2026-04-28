import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, tap } from 'rxjs';

const ROL_CLAIM = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
const ID_CLAIM = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);

  getToken(): string | null {
    if (typeof localStorage === 'undefined') return null;
    return localStorage.getItem('drivex_token') ?? sessionStorage.getItem('drivex_token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getName(): string {
    if (typeof localStorage === 'undefined') return 'Usuario';
    return localStorage.getItem('drivex_nombre') ?? sessionStorage.getItem('drivex_nombre') ?? 'Usuario';
  }

  getEmail(): string {
    if (typeof localStorage === 'undefined') return '';
    return localStorage.getItem('drivex_email') ?? sessionStorage.getItem('drivex_email') ?? '';
  }

  getRoles(): string[] {
    if (typeof localStorage === 'undefined') return [];
    const raw = localStorage.getItem('drivex_roles') ?? sessionStorage.getItem('drivex_roles');
    return raw ? JSON.parse(raw) : [];
  }

  // Retorna el CLI_id o USU_id guardado desde el JWT
  getClienteId(): string | null {
    if (typeof localStorage === 'undefined') return null;
    return localStorage.getItem('drivex_uid') ?? sessionStorage.getItem('drivex_uid');
  }

  isAdmin(): boolean { return this.getRoles().includes('ADMINISTRADOR'); }
  isVendedor(): boolean { return this.getRoles().includes('VENDEDOR'); }
  isCliente(): boolean {
    const roles = this.getRoles();
    return roles.includes('CLIENTE') || (!this.isAdmin() && !this.isVendedor());
  }

  login(correo: string, password: string, remember: boolean = true): Observable<any> {
    return this.http
      .post<any>(`${environment.apiUrl}/api/v1/auth/login`, { Correo: correo, Password: password })
      .pipe(
        tap((res: any) => {
          const token: string | undefined = res?.Data?.Token ?? res?.data?.token ?? res?.token;
          if (!token) {
            console.error('No se recibió token. Respuesta completa:', res);
            return;
          }
          this._saveSession(token, remember);
        })
      );
  }

  registrarCliente(data: {
    CLI_nombres: string;
    CLI_apellidos: string;
    CLI_cedula: string;
    CLI_telefono?: string;
  }): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/clientes`, data);
  }

  logout(): void {
    ['drivex_token', 'drivex_roles', 'drivex_email', 'drivex_nombre', 'drivex_uid'].forEach(k => {
      localStorage.removeItem(k);
      sessionStorage.removeItem(k);
    });
  }

  private _saveSession(token: string, remember: boolean): void {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));

      const rolRaw = payload[ROL_CLAIM];
      const rolesRaw: string[] = Array.isArray(rolRaw) ? rolRaw : (rolRaw ? [rolRaw] : ['CLIENTE']);
      const roles: string[] = rolesRaw.map(r => {
        const u = r.toUpperCase();
        if (u === 'ADMIN' || u === 'ADMINISTRADOR') return 'ADMINISTRADOR';
        if (u === 'VENDEDOR') return 'VENDEDOR';
        return u === 'CLIENTE' ? 'CLIENTE' : u;
      });

      const email: string = payload['email'] ?? '';
      const nombre: string = payload['name'] ?? payload['unique_name'] ?? email;
      // El sub / nameidentifier es el ID del usuario en la BD
      const uid: string = payload[ID_CLAIM] ?? payload['sub'] ?? '';

      const store = remember ? localStorage : sessionStorage;
      store.setItem('drivex_token', token);
      store.setItem('drivex_roles', JSON.stringify(roles));
      store.setItem('drivex_email', email);
      store.setItem('drivex_nombre', nombre);
      store.setItem('drivex_uid', uid);
    } catch (e) {
      console.error('Error al decodificar JWT:', e);
    }
  }
}
