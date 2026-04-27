import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-sucursales',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sucursales.html',
  styleUrls: ['./sucursales.scss']
})
export class SucursalesComponent implements OnInit {
  private http = inject(HttpClient);
  sucursales = signal<any[]>([]);

  ngOnInit() {
    this.cargarSucursales();
  }

  cargarSucursales() {
    this.http.get<any>(`${environment.apiUrl}/sucursales`).subscribe({
      next: (res) => this.sucursales.set(res.Data || res),
      error: (err) => console.error('Error al cargar sucursales', err)
    });
  }
}
