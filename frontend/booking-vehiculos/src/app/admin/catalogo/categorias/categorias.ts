import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-categorias',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './categorias.html',
  styleUrls: ['./categorias.scss']
})
export class CategoriasComponent implements OnInit {
  private http = inject(HttpClient);
  categorias = signal<any[]>([]);

  ngOnInit() {
    this.cargarCategorias();
  }

  cargarCategorias() {
    this.http.get<any>(`${environment.apiUrl}/api/v1/categorias-vehiculo`).subscribe(res => {
      this.categorias.set(res.Data || res);
    });
  }
}
