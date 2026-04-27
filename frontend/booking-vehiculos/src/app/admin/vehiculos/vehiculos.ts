import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-vehiculos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './vehiculos.html',
  styleUrls: ['./vehiculos.scss']
})
export class VehiculosComponent implements OnInit {
  private http = inject(HttpClient);
  vehiculos = signal<any[]>([]);
  isLoading = signal<boolean>(true);

  ngOnInit() {
    this.cargarVehiculos();
  }

  cargarVehiculos() {
    this.isLoading.set(true);
    this.http.get<any>(`${environment.apiUrl}/vehiculos`).subscribe({
      next: (res) => {
        this.vehiculos.set(res.Data || res);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error al cargar vehículos', err);
        this.isLoading.set(false);
      }
    });
  }

  eliminar(id: string) {
    if (!confirm('¿Seguro que deseas eliminar este vehículo?')) return;
    this.http.delete(`${environment.apiUrl}/vehiculos/${id}`).subscribe({
      next: () => this.cargarVehiculos(),
      error: (err) => console.error('Error al eliminar', err)
    });
  }
}
