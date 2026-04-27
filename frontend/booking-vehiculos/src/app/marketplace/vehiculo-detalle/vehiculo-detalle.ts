import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
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
  private router = inject(Router);
  private http = inject(HttpClient);

  vehiculo = signal<any>(null);

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.http.get<any>(`${environment.apiUrl}/vehiculos/${id}`).subscribe(res => {
        this.vehiculo.set(res.Data || res);
      });
    }
  }

  irAReservar() {
    this.router.navigate(['/reserva-wizard', this.vehiculo().VEH_id]);
  }
}
