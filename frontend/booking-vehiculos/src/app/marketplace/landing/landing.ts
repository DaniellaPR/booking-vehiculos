import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CardVehiculo } from '../../shared/components/card-vehiculo/card-vehiculo';
import { VehiculosService } from '../.././core/api'; // Swagger service
import { environment } from '../../../environments/environment';
import { NavbarComponent } from '../../shared/components/navbar/navbar';
import { FooterComponent } from '../../shared/components/footer/footer';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FooterComponent, CardVehiculo],
  templateUrl: './landing.html',
  styleUrls: ['./landing.scss']
})
export class LandingComponent implements OnInit {
  private vehiculosService = inject(VehiculosService);
  private http = inject(HttpClient);
  public router = inject(Router);

  // Señales de Estado
  isLoading = signal<boolean>(true);
  vehiculos = signal<any[]>([]);
  sucursales = signal<any[]>([]);
  categorias = signal<any[]>([]);

  // Filtros
  categoriaActiva = signal<string | null>(null);
  sucursalSeleccionada = signal<string>('');

  // Vehículos calculados automáticamente basados en los filtros
  vehiculosFiltrados = computed(() => {
    let filtrados = this.vehiculos();

    if (this.categoriaActiva()) {
      const catIdActiva = this.categoriaActiva();
      // Buscamos el nombre de la categoría a partir del ID activo
      const catObj = this.categorias().find(c => (c.CAT_id || c.caT_id) === catIdActiva);
      const nombreCat = catObj ? (catObj.CAT_nombre || catObj.caT_nombre) : null;

      filtrados = filtrados.filter(v => {
        return (v.categoriaId || v.CAT_id || v.caT_id) === catIdActiva || v.categoria === nombreCat;
      });
    }

    if (this.sucursalSeleccionada()) {
      filtrados = filtrados.filter(v =>
        v.sucursalId === this.sucursalSeleccionada() ||
        v.suC_id === this.sucursalSeleccionada() ||
        v.SUC_id === this.sucursalSeleccionada()
      );
    }

    return filtrados;
  });

  getPrecioVehiculo(vehiculo: any): number {
    if (!vehiculo) return 0;
    if (vehiculo.precioPorDia != null) return vehiculo.precioPorDia;
    if (vehiculo.categoria) {
      const categoria = this.categorias().find((c: any) => (c.CAT_nombre || c.caT_nombre) === vehiculo.categoria);
      if (categoria) return categoria.CAT_costoBase || categoria.caT_costoBase || 0;
    }
    return 0;
  }

  // Función sencilla para saber si el usuario ya inició sesión
  isLogged(): boolean {
    return !!localStorage.getItem('token'); // Devuelve true si existe un token
  }

  ngOnInit() {
    this.cargarDatos();
  }

  cargarDatos() {
    this.isLoading.set(true);

    // 1. Cargar Vehículos
    this.vehiculosService.apiV1VehiculosGet().subscribe({
      next: (res: any) => {
        this.vehiculos.set(res.data || res.Data || res || []);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error al cargar vehículos:', err);
        this.isLoading.set(false);
      }
    });

    // 2. Cargar Sucursales
    this.http.get<any>(`${environment.apiUrl}/api/v1/sucursales`).subscribe({
      next: (res) => this.sucursales.set(res.data || res.Data || res || []),
      error: (err) => console.error('Error sucursales:', err)
    });

    // 3. Cargar Categorías
    this.http.get<any>(`${environment.apiUrl}/api/v1/categorias-vehiculo`).subscribe({
      next: (res) => this.categorias.set(res.data || res.Data || res || []),
      error: (err) => console.error('Error categorias:', err)
    });
  }


  filtrarCategoria(catId: string | null) {
    this.categoriaActiva.set(catId);
  }

  onSucursalChange(event: any) {
    this.sucursalSeleccionada.set(event.target.value);
  }

  buscarVehiculos() {
    const element = document.getElementById('vehiculos');
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  }

  verTodos() {
    this.router.navigate(['/buscar']);
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }

  goToRegister() {
    this.router.navigate(['/registro']);
  }
}
