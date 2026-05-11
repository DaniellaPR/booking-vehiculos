// app/app/marketplace/busqueda/busqueda.ts
import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { NavbarComponent } from '../../shared/components/navbar/navbar';
import { FooterComponent } from '../../shared/components/footer/footer';
import { CardVehiculo } from '../../shared/components/card-vehiculo/card-vehiculo';
import { VehiculosService, TarifasService } from '../.././core/api';

@Component({
  selector: 'app-busqueda',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FooterComponent, CardVehiculo],
  templateUrl: './busqueda.html',
  styleUrls: ['./busqueda.scss']
})
export class BusquedaComponent implements OnInit {
  private vehiculosService = inject(VehiculosService);
  private tarifasSvc = inject(TarifasService);
  private http = inject(HttpClient);
  public router = inject(Router);

  vehiculos = signal<any[]>([]);
  categorias = signal<any[]>([]);
  tarifas = signal<any[]>([]);

  categoriaSeleccionada = signal<string>('');


  vehiculosFiltrados = computed(() => {
    const catId = this.categoriaSeleccionada();
    if (!catId) return this.vehiculos();

    // Encontramos el NOMBRE de la categoría seleccionada en el dropdown
    const catSeleccionada = this.categorias().find(c => (c.CAT_id || c.caT_id) === catId);
    const nombreCat = catSeleccionada ? (catSeleccionada.CAT_nombre || catSeleccionada.caT_nombre) : null;

    return this.vehiculos().filter(v => {
      // Filtramos coincidiendo el UUID (viejo formato) o el Nombre (nuevo formato)
      return (v.categoriaId || v.CAT_id || v.caT_id) === catId || v.categoria === nombreCat;
    });
  });

  getPrecioVehiculo(vehiculo: any): number {
    if (!vehiculo) return 0;
    if (vehiculo.precioPorDia != null) return vehiculo.precioPorDia;

    // Cruce por nombre
    if (vehiculo.categoria) {
      const cat = this.categorias().find((c: any) => (c.CAT_nombre || c.caT_nombre) === vehiculo.categoria);
      if (cat) return cat.CAT_costoBase || cat.caT_costoBase || 0;
    }
    return 0;
  }

  ngOnInit() {
    this.cargarCategorias();
    this.cargarVehiculos();
    this.cargarTarifas();
  }

  cargarCategorias() {
    this.http.get<any>(`${environment.apiUrl}/api/v1/categorias-vehiculo`).subscribe({
      next: (res) => this.categorias.set(res.Data || res.data || res || []),
      error: (err) => console.error('Error cargando categorías', err)
    });
  }

  cargarVehiculos() {
    this.vehiculosService.apiV1VehiculosGet().subscribe({
      next: (res: any) => this.vehiculos.set(res.Data || res.data || res || []),
      error: (err) => console.error('Error cargando vehículos', err)
    });
  }

  cargarTarifas() {
    this.tarifasSvc.apiV1TarifasGet().subscribe({
      next: (res: any) => this.tarifas.set(res.Data || res.data || []),
      error: (err) => console.error('Error cargando tarifas', err)
    });
  }

  

  onCategoriaChange(event: any) {
    this.categoriaSeleccionada.set(event.target.value);
  }

  verDetalle(id: string) {
    this.router.navigate(['/vehiculo', id]);
  }
}
