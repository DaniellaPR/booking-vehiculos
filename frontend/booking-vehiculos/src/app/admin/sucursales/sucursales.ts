import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-admin-sucursales',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sucursales.html',
  styleUrls: ['./sucursales.scss']
})
export class SucursalesComponent implements OnInit {
  private http = inject(HttpClient);

  sucursales = signal<any[]>([]);
  isLoading = signal<boolean>(true);

  // Modal y Estado
  mostrarModal = signal(false);
  guardando = signal(false);
  errorModal = signal<string | null>(null);
  modoEdicion = signal(false);
  sucursalSeleccionadaId = signal<string | null>(null);

  form = {
    SUC_nombre: '',
    SUC_ciudad: '',
    SUC_direccion: '',
    SUC_coordenadas: ''
  };

  ngOnInit() {
    this.cargarSucursales();
  }

  cargarSucursales() {
    this.isLoading.set(true);
    this.http.get<any>(`${environment.apiUrl}/api/v1/sucursales`).subscribe({
      next: (res) => {
        this.sucursales.set(res.Data || res.data || []);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error al cargar sucursales', err);
        this.isLoading.set(false);
      }
    });
  }

  abrirModalNuevo() {
    this.modoEdicion.set(false);
    this.sucursalSeleccionadaId.set(null);
    this.form = { SUC_nombre: '', SUC_ciudad: '', SUC_direccion: '', SUC_coordenadas: '' };
    this.errorModal.set(null);
    this.mostrarModal.set(true);
  }

  abrirModalEditar(sucursal: any) {
    this.modoEdicion.set(true);
    this.sucursalSeleccionadaId.set(sucursal.SUC_id || sucursal.suC_id);
    this.form = {
      SUC_nombre: sucursal.SUC_nombre || sucursal.suC_nombre,
      SUC_ciudad: sucursal.SUC_ciudad || sucursal.suC_ciudad,
      SUC_direccion: sucursal.SUC_direccion || sucursal.suC_direccion,
      SUC_coordenadas: sucursal.SUC_coordenadas || sucursal.suC_coordenadas || ''
    };
    this.errorModal.set(null);
    this.mostrarModal.set(true);
  }

  cerrarModal() {
    this.mostrarModal.set(false);
  }

  guardar() {
    if (!this.form.SUC_nombre || !this.form.SUC_ciudad || !this.form.SUC_direccion) {
      this.errorModal.set('Nombre, ciudad y dirección son obligatorios.');
      return;
    }
    this.guardando.set(true);
    this.errorModal.set(null);

    if (this.modoEdicion()) {
      // Petición PUT para Editar
      this.http.put<any>(`${environment.apiUrl}/api/v1/sucursales/${this.sucursalSeleccionadaId()}`, this.form).subscribe({
        next: (res) => {
          const actualizada = res.Data || res.data || { ...this.form, SUC_id: this.sucursalSeleccionadaId() };
          this.sucursales.update(s => s.map(x => (x.SUC_id || x.suC_id) === this.sucursalSeleccionadaId() ? actualizada : x));
          this.guardando.set(false);
          this.mostrarModal.set(false);
        },
        error: (err) => {
          this.guardando.set(false);
          this.errorModal.set(err.error?.Message || err.error?.message || 'Error al actualizar sucursal.');
        }
      });
    } else {
      // Petición POST para Crear
      this.http.post<any>(`${environment.apiUrl}/api/v1/sucursales`, this.form).subscribe({
        next: (res) => {
          const nueva = res.Data || res.data;
          if (nueva) this.sucursales.update(s => [...s, nueva]);
          this.guardando.set(false);
          this.mostrarModal.set(false);
        },
        error: (err) => {
          this.guardando.set(false);
          this.errorModal.set(err.error?.Message || err.error?.message || 'Error al crear sucursal.');
        }
      });
    }
  }
}
