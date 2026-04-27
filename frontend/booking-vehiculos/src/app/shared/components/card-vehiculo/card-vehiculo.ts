import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-card-vehiculo',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './card-vehiculo.html',
  styleUrl: './card-vehiculo.scss',
})
export class CardVehiculo {
  @Input() vehiculo: any; // Esto es vital para recibir la info de la BD
}
