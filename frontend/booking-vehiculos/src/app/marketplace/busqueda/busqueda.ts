import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { NavbarComponent } from '../../shared/components/navbar/navbar';
import { FooterComponent } from '../../shared/components/footer/footer';

@Component({
  selector: 'app-busqueda',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FooterComponent],
  templateUrl: './busqueda.html',
  styleUrls: ['./busqueda.scss']
})
export class BusquedaComponent implements OnInit {
  private http = inject(HttpClient);
  private router = inject(Router);

  vehiculos = signal<any[]>([]);
  categorias = signal<any[]>([]);

  ngOnInit() {
    this.cargarCategorias();
    this.cargarVehiculos();
  }

  cargarCategorias() {
    this.http.get<any>(`${environment.apiUrl}/categorias-vehiculo`).subscribe(res => {
      this.categorias.set(res.Data || res);
    });
  }

  cargarVehiculos() {
    this.http.get<any>(`${environment.apiUrl}/vehiculos`).subscribe(res => {
      this.vehiculos.set(res.Data || res);
    });
  }

  verDetalle(id: string) {
    this.router.navigate(['/vehiculo', id]);
  }
}
