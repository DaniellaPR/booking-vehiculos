import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-extras',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './extras.html',
  styleUrls: ['./extras.scss']
})
export class ExtrasComponent implements OnInit {
  private http = inject(HttpClient);
  extras = signal<any[]>([]);

  ngOnInit() {
    this.cargarExtras();
  }

  cargarExtras() {
    this.http.get<any>(`${environment.apiUrl}/extras-adicionales`).subscribe(res => {
      this.extras.set(res.Data || res);
    });
  }
}
