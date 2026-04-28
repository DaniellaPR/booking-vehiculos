import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-clientes',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './clientes.html',
  styleUrls: ['./clientes.scss']
})
export class ClientesComponent implements OnInit {
  private http = inject(HttpClient);
  clientes = signal<any[]>([]);
  isLoading = signal<boolean>(true);

  ngOnInit() {
    this.cargarClientes();
  }

  cargarClientes() {
    this.isLoading.set(true);
    this.http.get<any>(`${environment.apiUrl}/api/v1/clientes`).subscribe({
      next: (res) => {
        this.clientes.set(res.Data || res);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error al cargar clientes', err);
        this.isLoading.set(false);
      }
    });
  }

  verHistorial(id: string) {
    alert('Próximamente: Historial de rentas del cliente ' + id);
  }
}
