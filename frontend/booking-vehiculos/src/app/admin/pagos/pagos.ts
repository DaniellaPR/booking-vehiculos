import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
// Importamos todo directamente desde el index (el barril) autogenerado
import { PagosService, PagoResponse } from '../../core/api';

@Component({
  selector: 'app-pagos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pagos.html',
  styleUrls: ['./pagos.scss'] // Cambia a .css si no usas scss
})
export class PagosComponent implements OnInit {
  // Inyectamos el servicio que acabas de mostrarme
  private pagosService = inject(PagosService);

  // Arreglo para guardar los datos de la tabla
  pagos: PagoResponse[] = [];
  cargando = true;
  error = '';

  ngOnInit(): void {
    this.cargarPagos();
  }

  cargarPagos(): void {
    this.cargando = true;
    this.error = '';

    // Llamamos al método exacto del PagosService
    this.pagosService.apiV1PagosGet().subscribe({
      next: (response: any) => {
        // OpenAPI a veces cambia la primera letra a minúscula en TypeScript (success vs Success)
        // Hacemos una validación segura para ambos casos:
        const success = response.success !== undefined ? response.success : response.Success;
        const data = response.data !== undefined ? response.data : response.Data;

        if (success && data) {
          this.pagos = data;
        } else {
          this.error = 'No se encontraron pagos en el sistema.';
        }
        this.cargando = false;
      },
      error: (err) => {
        console.error('Error de red o de API al cargar pagos:', err);
        this.error = 'Error de conexión con el servidor. Intenta nuevamente.';
        this.cargando = false;
      }
    });
  }
}
