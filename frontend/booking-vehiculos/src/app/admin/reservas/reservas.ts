import { Component, OnInit, inject, signal } from '@angular/core'; // CORREGIDO AQUÍ
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-reservas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reservas.html',
  styleUrls: ['./reservas.scss']
})
export class ReservasComponent implements OnInit {
  private http = inject(HttpClient);
  reservas = signal<any[]>([]);

  ngOnInit() {
    this.cargarReservas();
  }

  cargarReservas() {
    this.http.get<any>(`${environment.apiUrl}/reservas`).subscribe({
      next: (res) => this.reservas.set(res.Data || res),
      error: (err) => console.error('Error al cargar reservas', err)
    });
  }

  cambiarEstado(id: string, nuevoEstado: string) {
    // Aquí llamarías a un endpoint de actualización parcial o PUT
    alert(`Cambiando reserva ${id} a estado: ${nuevoEstado}`);
    // this.http.patch(`${environment.apiUrl}/reservas/${id}/estado`, { estado: nuevoEstado }).subscribe(...)
  }
}
