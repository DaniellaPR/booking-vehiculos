import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../shared/components/navbar/navbar';

@Component({
  selector: 'app-reserva-wizard',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './reserva-wizard.html',
  styleUrls: ['./reserva-wizard.scss']
})
export class ReservaWizardComponent {
  step = signal<number>(1);

  next() { this.step.update(s => s + 1); }
  prev() { this.step.update(s => s - 1); }

  finalizar() {
    alert('Reserva enviada con éxito');
  }
}
