import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../core/auth/auth.service';
import { UsuariosAppService } from '../../core/api/api/usuariosApp.service';
import { RolesService } from '../../core/api/api/roles.service';

// ROL ids del seed
const ROL_IDS: Record<string, string> = {
  'ADMINISTRADOR': '77777777-0000-0000-0000-000000000001',
  'VENDEDOR': '77777777-0000-0000-0000-000000000002',
  'CLIENTE': '77777777-0000-0000-0000-000000000003',
};

interface Usuario {
  USU_id?: string;
  uSU_id?: string;
  ROL_id?: string;
  rOL_id?: string;
  USU_email?: string;
  uSU_email?: string;
  USU_fechaCreacion?: string;
  uSU_fechaCreacion?: string;
  // enriched
  _rolNombre?: string;
  _editando?: boolean;
  _rolSeleccionado?: string;
}

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './usuarios.html',
  styleUrls: ['./usuarios.scss']
})
export class UsuariosComponent implements OnInit {
  private http = inject(HttpClient);
  private auth = inject(AuthService);
  private usuSvc = inject(UsuariosAppService);
  private rolesSvc = inject(RolesService);

  isLoading = signal(true);
  isSaving = signal(false);
  globalMsg = signal<{ text: string; tipo: 'ok' | 'err' } | null>(null);

  usuarios = signal<Usuario[]>([]);
  roles = signal<any[]>([]);

  // Filtro
  filtroRol = '';
  filtroEmail = '';

  // Crear nuevo admin/vendedor
  showCrear = false;
  nuevoEmail = '';
  nuevoPassword = '';
  nuevoRol = '77777777-0000-0000-0000-000000000001'; // ADMIN por defecto
  errorCrear = '';

  // Confirmación de eliminación
  usuarioAEliminar: Usuario | null = null;

  private base = `${environment.apiUrl}/api/v1`;

  ngOnInit() { this.cargar(); }

  cargar() {
    this.isLoading.set(true);
    forkJoin({
      usuarios: this.http.get<any>(`${this.base}/usuarios-app`),
      roles: this.http.get<any>(`${this.base}/roles`),
    }).subscribe({
      next: ({ usuarios, roles }) => {
        const rolesArr = roles.Data || roles.data || [];
        this.roles.set(rolesArr);
        const usArr: Usuario[] = (usuarios.Data || usuarios.data || []).map((u: Usuario) => {
          const rId = u.ROL_id || u.rOL_id || '';
          const rol = rolesArr.find((r: any) => (r.ROL_id || r.rOL_id) === rId);
          return {
            ...u,
            _rolNombre: rol ? (rol.ROL_nombre || rol.rOL_nombre) : '—',
            _editando: false,
            _rolSeleccionado: rId
          };
        });
        this.usuarios.set(usArr);
        this.isLoading.set(false);
      },
      error: () => {
        this.isLoading.set(false);
        this.mostrarMsg('Error al cargar usuarios.', 'err');
      }
    });
  }

  get usuariosFiltrados(): Usuario[] {
    return this.usuarios().filter(u => {
      const email = (u.USU_email || u.uSU_email || '').toLowerCase();
      const rolId = u.ROL_id || u.rOL_id || '';
      const pasaEmail = !this.filtroEmail || email.includes(this.filtroEmail.toLowerCase());
      const pasaRol = !this.filtroRol || rolId === this.filtroRol;
      return pasaEmail && pasaRol;
    });
  }

  iniciarEdicion(u: Usuario) {
    // Solo un usuario en edición a la vez
    this.usuarios.update(arr => arr.map(x => ({ ...x, _editando: false })));
    u._editando = true;
    u._rolSeleccionado = u.ROL_id || u.rOL_id || '';
    this.usuarios.update(arr => arr.map(x => (x.USU_id || x.uSU_id) === (u.USU_id || u.uSU_id) ? { ...u } : x));
  }

  cancelarEdicion(u: Usuario) {
    u._editando = false;
    this.usuarios.update(arr => arr.map(x => (x.USU_id || x.uSU_id) === (u.USU_id || u.uSU_id) ? { ...u } : x));
  }

  guardarRol(u: Usuario) {
    const id = u.USU_id || u.uSU_id;
    if (!id) return;

    const nuevoRolId = u._rolSeleccionado!;
    if (nuevoRolId === (u.ROL_id || u.rOL_id)) {
      this.cancelarEdicion(u);
      return;
    }

    this.isSaving.set(true);
    this.http.put<any>(`${this.base}/usuarios-app/${id}`, {
      ROL_id: nuevoRolId,
      USU_email: u.USU_email || u.uSU_email,
      USU_usuarioModificacion: this.auth.getEmail()
    }).subscribe({
      next: () => {
        this.isSaving.set(false);
        this.mostrarMsg('Rol actualizado correctamente.', 'ok');
        this.cargar();
      },
      error: (err) => {
        this.isSaving.set(false);
        this.mostrarMsg(err.error?.Message || 'Error al actualizar el rol.', 'err');
      }
    });
  }

  confirmarEliminar(u: Usuario) {
    this.usuarioAEliminar = u;
  }

  cancelarEliminar() {
    this.usuarioAEliminar = null;
  }

  eliminar() {
    const u = this.usuarioAEliminar;
    if (!u) return;
    const id = u.USU_id || u.uSU_id;
    this.isSaving.set(true);
    this.http.delete<any>(`${this.base}/usuarios-app/${id}`).subscribe({
      next: () => {
        this.isSaving.set(false);
        this.usuarioAEliminar = null;
        this.mostrarMsg('Usuario eliminado.', 'ok');
        this.cargar();
      },
      error: (err) => {
        this.isSaving.set(false);
        this.mostrarMsg(err.error?.Message || 'Error al eliminar.', 'err');
      }
    });
  }

  crearUsuario() {
    this.errorCrear = '';
    if (!this.nuevoEmail.trim() || !this.nuevoPassword.trim()) {
      this.errorCrear = 'Correo y contraseña son obligatorios.';
      return;
    }
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.nuevoEmail.trim())) {
      this.errorCrear = 'Correo inválido.';
      return;
    }
    if (this.nuevoPassword.length < 8) {
      this.errorCrear = 'La contraseña debe tener al menos 8 caracteres.';
      return;
    }

    this.isSaving.set(true);
    this.http.post<any>(`${this.base}/usuarios-app`, {
      ROL_id: this.nuevoRol,
      USU_email: this.nuevoEmail.trim().toLowerCase(),
      USU_passwordHash: this.nuevoPassword,
      USU_usuarioCreacion: this.auth.getEmail()
    }).subscribe({
      next: () => {
        this.isSaving.set(false);
        this.showCrear = false;
        this.nuevoEmail = '';
        this.nuevoPassword = '';
        this.mostrarMsg('Usuario creado exitosamente.', 'ok');
        this.cargar();
      },
      error: (err) => {
        this.isSaving.set(false);
        this.errorCrear = err.error?.Message || err.error?.message || 'Error al crear usuario.';
      }
    });
  }

  getNombreRol(rolId: string): string {
    const r = this.roles().find((x: any) => (x.ROL_id || x.rOL_id) === rolId);
    return r ? (r.ROL_nombre || r.rOL_nombre) : '—';
  }

  getRolClass(nombre: string): string {
    const n = (nombre || '').toUpperCase();
    if (n === 'ADMINISTRADOR') return 'badge-admin';
    if (n === 'VENDEDOR') return 'badge-vendedor';
    return 'badge-cliente';
  }

  formatFecha(f: string): string {
    if (!f) return '—';
    return new Date(f).toLocaleDateString('es-EC', { day: '2-digit', month: 'short', year: 'numeric' });
  }

  private mostrarMsg(text: string, tipo: 'ok' | 'err') {
    this.globalMsg.set({ text, tipo });
    setTimeout(() => this.globalMsg.set(null), 4000);
  }

  get esMiPropioUsuario() {
    return (u: Usuario) => (u.USU_email || u.uSU_email) === this.auth.getEmail();
  }
}
