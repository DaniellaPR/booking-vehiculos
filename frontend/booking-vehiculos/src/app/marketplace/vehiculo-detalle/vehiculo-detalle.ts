import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
// Asegúrate de que los nombres de los servicios coincidan con los de tu core/api
import { VehiculosService, CategoriasVehiculoService } from '../.././core/api';
import { NavbarComponent } from '../../shared/components/navbar/navbar';

@Component({
  selector: 'app-vehiculo-detalle',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './vehiculo-detalle.html',
  styleUrls: ['./vehiculo-detalle.scss']
})
export class VehiculoDetalleComponent implements OnInit {
  private route = inject(ActivatedRoute);
  public router = inject(Router);

  private vehiculosService = inject(VehiculosService);
  // Inyectamos el servicio de Categorías
  private categoriasSvc = inject(CategoriasVehiculoService);

  vehiculo = signal<any>(null);
  categorias = signal<any[]>([]); // Guardará todas las categorías

  isLoading = signal<boolean>(true);
  errorMessage = signal<string | null>(null);

  // LA MAGIA: Buscamos la categoría del vehículo y extraemos su precio real
  precioBase = computed(() => {
    const v = this.vehiculo();
    if (!v) return 0;

    const catId = v.CAT_id || v.caT_id;
    const categoria = this.categorias().find((c: any) => (c.CAT_id || c.caT_id) === catId);

    return categoria ? (categoria.CAT_costoBase || categoria.caT_costoBase || 0) : 0;
  });

  ngOnInit() {
    // 1. Cargamos las categorías en memoria
    this.categoriasSvc.apiV1CategoriasVehiculoGet().subscribe({
      next: (res: any) => this.categorias.set(res.Data || res.data || [])
    });

    // 2. Cargamos el detalle del vehículo
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.cargarDetalle(id);
    } else {
      this.errorMessage.set("ID de vehículo no proporcionado.");
      this.isLoading.set(false);
    }
  }

  cargarDetalle(id: string) {
    this.isLoading.set(true);
    this.vehiculosService.apiV1VehiculosIdGet(id).subscribe({
      next: (res: any) => {
        this.isLoading.set(false);
        this.vehiculo.set(res.Data || res.data || res);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.errorMessage.set('No se pudo cargar la información del vehículo.');
      }
    });
  }

  irAReservar() {
    const v = this.vehiculo();
    if (v) this.router.navigate(['/reservar', v.VEH_id || v.veH_id]);
  }
}
