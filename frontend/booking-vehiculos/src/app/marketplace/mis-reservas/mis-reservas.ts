import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NavbarComponent } from '../../shared/components/navbar/navbar';
import { FooterComponent } from '../../shared/components/footer/footer';
import { AuthService } from '../../core/auth/auth.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-mis-reservas',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FooterComponent],
  templateUrl: './mis-reservas.html',
  styleUrls: ['./mis-reservas.scss']
})
export class MisReservasComponent implements OnInit {
  private authService = inject(AuthService);
  private http = inject(HttpClient);
  public router = inject(Router);

  reservas = signal<any[]>([]);
  isLoading = signal<boolean>(true);
  errorMsg = signal<string>('');

  ngOnInit() {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }
    this.cargarReservas();
  }

  cargarReservas() {
    this.isLoading.set(true);
    // Trae todas las reservas y filtra por CLI_id del usuario logueado
    this.http.get<any>(`${environment.apiUrl}/api/v1/reservas`).subscribe({
      next: (res) => {
        const todas: any[] = res.Data || res.data || [];
        const miId = this.authService.getClienteId();
        // Si hay CLI_id guardado en sesión, filtramos; si no, mostramos todas (admin)
        const mias = miId
          ? todas.filter(r => (r.CLI_id || r.cLI_id) === miId)
          : todas;
        this.reservas.set(mias);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error cargando reservas:', err);
        this.errorMsg.set('No se pudieron cargar tus reservas.');
        this.isLoading.set(false);
      }
    });
  }

  getBadgeClass(estado: string): string {
    const map: Record<string, string> = {
      'Confirmada': 'badge-confirmada',
      'Pendiente': 'badge-pendiente',
      'Cancelada': 'badge-cancelada',
    };
    return map[estado] || 'badge-pendiente';
  }

  formatFecha(f: string): string {
    if (!f) return '—';
    return new Date(f).toLocaleDateString('es-EC', { day: '2-digit', month: 'short', year: 'numeric' });
  }

  get nombre() { return this.authService.getName(); }
}
