import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class DashboardComponent {
  // Simulamos datos hasta conectar el API de estadísticas
  stats = signal([
    { label: 'Vehículos Disponibles', value: '18', icon: '🚗', trend: '+2' },
    { label: 'Reservas Activas', value: '45', icon: '📅', trend: '+5' },
    { label: 'Clientes Totales', value: '128', icon: '👥', trend: '+12' }
  ]);
}
