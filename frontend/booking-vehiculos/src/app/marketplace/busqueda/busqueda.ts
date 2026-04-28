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
    return this.vehiculos().filter(v => (v.caT_id || v.CAT_id) === catId);
  });

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

  // Obtiene el precio diario cruzando Tarifa → CAT_id del vehículo
  getPrecioVehiculo(vehiculo: any): number {
    if (!vehiculo) return 0;
    const catId = vehiculo.CAT_id || vehiculo.caT_id;
    const tarifa = this.tarifas().find(t => (t.CAT_id || t.caT_id) === catId);
    if (tarifa) return tarifa.TAR_precioDiario || tarifa.taR_precioDiario || 0;
    // Fallback: buscar en categorías si tienen costoBase
    const cat = this.categorias().find(c => (c.CAT_id || c.caT_id) === catId);
    return cat ? (cat.CAT_costoBase || cat.caT_costoBase || 0) : 0;
  }

  onCategoriaChange(event: any) {
    this.categoriaSeleccionada.set(event.target.value);
  }

  verDetalle(id: string) {
    this.router.navigate(['/vehiculo', id]);
  }
}
