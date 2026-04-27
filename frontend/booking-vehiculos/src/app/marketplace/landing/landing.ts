import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';

import { NavbarComponent } from '../../shared/components/navbar/navbar';
import { FooterComponent } from '../../shared/components/footer/footer';
// 1. IMPORTACIÓN CORREGIDA: Apunta a tu carpeta card-vehiculo y extrae la clase CardVehiculo
import { CardVehiculo } from '../../shared/components/card-vehiculo/card-vehiculo';

@Component({
  selector: 'app-landing',
  standalone: true,
  // 2. INCLUSIÓN CORREGIDA: Usamos CardVehiculo aquí
  imports: [CommonModule, NavbarComponent, FooterComponent, CardVehiculo],
  templateUrl: './landing.html',
  styleUrls: ['./landing.scss']
})
export class LandingComponent implements OnInit {
  private http = inject(HttpClient);
  private router = inject(Router);

  vehiculos = signal<any[]>([]);
  isLoading = signal<boolean>(true);
  errorMessage = signal<string | null>(null);

  ngOnInit() {
    this.cargarVehiculos();
  }

  cargarVehiculos() {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.http.get<any>(`${environment.apiUrl}/vehiculos`).subscribe({
      next: (res) => {
        this.isLoading.set(false);
        this.vehiculos.set(res.Data || res);
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        console.error('Error al cargar vehículos:', err);
        this.errorMessage.set('No se pudo conectar con el servidor.');
      }
    });
  }

  buscarVehiculos() {
    console.log("Buscar autos clickeado");
  }

  verTodos() {
    this.router.navigate(['/buscar']);
  }

  filtrarCategoria(categoria: any, evt: any) {
    console.log("Filtrar por", categoria);
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }

  goToRegister() {
    this.router.navigate(['/login']);
  }
}
