import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-admin-reservas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reservas.html',
  styleUrls: ['./reservas.scss']
})
export class ReservasComponent implements OnInit {
  private http = inject(HttpClient);

  reservas = signal<any[]>([]);
  isLoading = signal<boolean>(true);

  ngOnInit() {
    this.cargarReservas();
  }

  cargarReservas() {
    this.isLoading.set(true);
    // Solucionado el error 404 agregando /api/v1/
    this.http.get<any>(`${environment.apiUrl}/api/v1/reservas`).subscribe({
      next: (res) => {
        this.reservas.set(res.Data || res.data || []);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error al cargar reservas', err);
        this.isLoading.set(false);
      }
    });
  }

  cambiarEstado(reserva: any, nuevoEstado: string) {
    if (!confirm(`¿Estás seguro de cambiar esta reserva a estado: ${nuevoEstado}?`)) {
      return;
    }

    const id = reserva.RES_id || reserva.reS_id;
    // Preparamos el payload con el estado actualizado
    const payload = { ...reserva, RES_estado: nuevoEstado };

    // Usamos PUT para actualizar la reserva completa con el nuevo estado
    this.http.put(`${environment.apiUrl}/api/v1/reservas/${id}`, payload).subscribe({
      next: () => {
        // Actualizamos la tabla visualmente sin necesidad de recargar la página
        this.reservas.update(rs =>
          rs.map(r => (r.RES_id || r.reS_id) === id ? { ...r, RES_estado: nuevoEstado, reS_estado: nuevoEstado } : r)
        );
      },
      error: (err) => {
        console.error('Error al cambiar estado:', err);
        alert('Hubo un error al intentar actualizar el estado de la reserva. Verifica la consola.');
      }
    });
  }
}
