import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-seguros',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './seguros.html',
  styleUrls: ['./seguros.scss']
})
export class SegurosComponent implements OnInit {
  private http = inject(HttpClient);
  seguros = signal<any[]>([]);

  ngOnInit() {
    this.cargarSeguros();
  }

  cargarSeguros() {
    this.http.get<any>(`${environment.apiUrl}/seguros`).subscribe(res => {
      this.seguros.set(res.Data || res);
    });
  }
}
