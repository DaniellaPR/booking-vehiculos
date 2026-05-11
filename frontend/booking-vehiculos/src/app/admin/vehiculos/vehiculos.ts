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

  mostrarModal = signal(false);
  guardando = signal(false);
  errorModal = signal<string | null>(null);

  // FIX: VEH_imagenUrl incluido para poder guardarlo desde el modal
  form = {
    VEH_placa: '',
    VEH_modelo: '',
    VEH_anio: new Date().getFullYear(),
    VEH_color: '',
    VEH_kilometraje: 0,
    VEH_estado: 'Disponible',
    CAT_id: '',
    SUC_id: '',
    VEH_imagenUrl: ''   // ← campo añadido
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

  // ... (mantén tus importaciones y variables igual)

  getPrecio(veh: any): number {
    if (!veh) return 0;
    // 1. Si el backend ya lo envía (cumpliendo el contrato ideal)
    if (veh.precioPorDia != null) return veh.precioPorDia;

    // 2. Si no viene, cruzamos por el NOMBRE de la categoría
    if (veh.categoria) {
      const cat = this.categorias().find((c: any) => (c.CAT_nombre || c.caT_nombre) === veh.categoria);
      if (cat) return cat.CAT_costoBase || cat.caT_costoBase || 0;
    }

    // 3. Fallback por si en el futuro vuelve a usar CAT_id
    const catId = veh.categoriaId || veh.CAT_id || veh.caT_id;
    if (catId) {
      const cat = this.categorias().find((c: any) => (c.CAT_id || c.caT_id) === catId);
      if (cat) return cat.CAT_costoBase || cat.caT_costoBase || 0;
    }
    return 0;
  }

  guardar() {
    if (!this.form.VEH_placa || !this.form.VEH_modelo || !this.form.CAT_id || !this.form.SUC_id) {
      this.errorModal.set('Placa, modelo, categoría y sucursal son obligatorios.');
      return;
    }
    this.guardando.set(true);
    this.errorModal.set(null);

    // Mapeo Inteligente: Enviamos ambos formatos para asegurar compatibilidad 
    // sin importar cómo generó OpenAPI el request de creación
    const payload = {
      ...this.form,
      placa: this.form.VEH_placa,
      modelo: this.form.VEH_modelo,
      anio: this.form.VEH_anio,
      color: this.form.VEH_color,
      kilometraje: this.form.VEH_kilometraje,
      estado: this.form.VEH_estado,
      imagenUrl: this.form.VEH_imagenUrl,
      categoriaId: this.form.CAT_id,
      sucursalId: this.form.SUC_id
    };

    this.http.post<any>(`${environment.apiUrl}/api/v1/vehiculos`, payload).subscribe({
      next: (res) => {
        const nuevo = res.Data || res.data;
        if (nuevo) this.vehiculos.update(vs => [...vs, nuevo]);
        this.guardando.set(false);
        this.mostrarModal.set(false);
      },
      error: (err) => {
        this.guardando.set(false);
        this.errorModal.set(err.error?.Message || err.error?.message || 'Error al crear vehículo.');
      }
    });
  }

  getImagen(v: any): string {
    return v.VEH_imagenUrl || v.veH_imagenUrl ||
      'https://bryxtfwmhpbnlywibuhx.supabase.co/storage/v1/object/public/ImagenesAutos/auto-default.jpg';
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
      SUC_id: '',
      VEH_imagenUrl: ''
    };
    this.errorModal.set(null);
    this.mostrarModal.set(true);
  }

  cerrarModal() {
    this.mostrarModal.set(false);
  }

  

  eliminar(id: string) {
    if (confirm('⚠️ ¿Estás seguro de que deseas eliminar este vehículo de forma permanente?')) {
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
