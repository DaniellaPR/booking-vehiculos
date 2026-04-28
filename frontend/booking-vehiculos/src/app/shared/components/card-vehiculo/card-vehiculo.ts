import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-card-vehiculo',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './card-vehiculo.html',
  styleUrls: ['./card-vehiculo.scss'], // Ojo: es styleUrls con 's' al final
})
export class CardVehiculo {
  @Input() vehiculo: any; // Esto es vital para recibir la info de la BD

  // 💥 ESTA ES LA LÍNEA QUE FALTABA:
  @Input() precioBase: number = 0;
}
