import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { VehiculosService } from '../../core/api/api/vehiculos.service';
import { CategoriasVehiculoService } from '../../core/api/api/categoriasVehiculo.service';

@Component({
  selector: 'app-admin-vehiculos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './vehiculos.html',
  styleUrls: ['./vehiculos.scss']
})
export class VehiculosComponent implements OnInit {
  private vehiculosSvc = inject(VehiculosService);
  private categoriasSvc = inject(CategoriasVehiculoService);
  private http = inject(HttpClient);

  vehiculos = signal<any[]>([]);
  categorias = signal<any[]>([]);
  sucursales = signal<any[]>([]);
  isLoading = signal<boolean>(true);

  // Modal
  mostrarModal = signal(false);
  guardando = signal(false);
  errorModal = signal<string | null>(null);

  form = {
    VEH_placa: '',
    VEH_modelo: '',
    VEH_anio: new Date().getFullYear(),
    VEH_color: '',
    VEH_kilometraje: 0,
    VEH_estado: 'Disponible',
    CAT_id: '',
    SUC_id: ''
  };

  ngOnInit() {
    this.cargarDatos();
  }

  cargarDatos() {
    this.isLoading.set(true);
    this.categoriasSvc.apiV1CategoriasVehiculoGet().subscribe({
      next: (res: any) => {
        this.categorias.set(res.Data || res.data || []);
        this.vehiculosSvc.apiV1VehiculosGet().subscribe({
          next: (resVeh: any) => {
            this.vehiculos.set(resVeh.Data || resVeh.data || []);
            this.isLoading.set(false);
          }
        });
      }
    });
    this.http.get<any>(`${environment.apiUrl}/api/v1/sucursales`).subscribe({
      next: (res) => this.sucursales.set(res.Data || res.data || [])
    });
  }

  getPrecio(v: any): number {
    const catId = v.CAT_id || v.caT_id;
    const cat = this.categorias().find(c => (c.CAT_id || c.caT_id) === catId);
    return cat ? (cat.CAT_costoBase || cat.caT_costoBase || 0) : 0;
  }

  abrirModal() {
    this.form = {
      VEH_placa: '',
      VEH_modelo: '',
      VEH_anio: new Date().getFullYear(),
      VEH_color: '',
      VEH_kilometraje: 0,
      VEH_estado: 'Disponible',
      CAT_id: '',
      SUC_id: ''
    };
    this.errorModal.set(null);
    this.mostrarModal.set(true);
  }

  cerrarModal() {
    this.mostrarModal.set(false);
  }

  guardar() {
    if (!this.form.VEH_placa || !this.form.VEH_modelo || !this.form.CAT_id || !this.form.SUC_id) {
      this.errorModal.set('Placa, modelo, categoría y sucursal son obligatorios.');
      return;
    }
    this.guardando.set(true);
    this.errorModal.set(null);

    this.http.post<any>(`${environment.apiUrl}/api/v1/vehiculos`, this.form).subscribe({
      next: (res) => {
        const nuevo = res.Data || res.data;
        if (nuevo) this.vehiculos.update(v => [...v, nuevo]);
        this.guardando.set(false);
        this.mostrarModal.set(false);
      },
      error: (err) => {
        this.guardando.set(false);
        this.errorModal.set(err.error?.Message || err.error?.message || 'Error al crear vehículo.');
      }
    });
  }

  eliminar(id: string) {
    if (confirm('⚠️ ¿Estás seguro de que deseas eliminar este vehículo de forma permanente?')) {
      // Usamos HttpClient directo en lugar del servicio autogenerado
      this.http.delete(`${environment.apiUrl}/api/v1/vehiculos/${id}`).subscribe({
        next: () => {
          this.vehiculos.update(v => v.filter(x => (x.VEH_id || x.veH_id) !== id));
        },
        error: (err: any) => {
          console.error('Error al eliminar:', err);
          alert('Hubo un error al intentar eliminar el vehículo.');
        }
      });
    }
  }
}
