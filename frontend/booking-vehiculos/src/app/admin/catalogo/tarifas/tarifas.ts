import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-tarifas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tarifas.html',
  styleUrls: ['./tarifas.scss']
})
export class TarifasComponent implements OnInit {
  private http = inject(HttpClient);
  tarifas = signal<any[]>([]);

  ngOnInit() {
    this.cargarTarifas();
  }

  cargarTarifas() {
    this.http.get<any>(`${environment.apiUrl}/api/v1/tarifas`).subscribe(res => {
      this.tarifas.set(res.Data || res);
    });
  }
}
