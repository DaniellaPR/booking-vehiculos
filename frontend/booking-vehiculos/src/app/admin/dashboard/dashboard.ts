import { Component, OnInit, inject, signal, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { forkJoin } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../.././environments/environment';
import { AuthService } from '../.././core/auth/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class DashboardComponent implements OnInit, AfterViewInit {
  private http = inject(HttpClient);
  private router = inject(Router);
  public auth = inject(AuthService);

  @ViewChild('donutCanvas') donutCanvas!: ElementRef<HTMLCanvasElement>;

  isLoading = signal(true);
  isRefreshing = signal(false);
  lastUpdate = signal('');

  // Datos reales
  vehiculos = signal<any[]>([]);
  reservas = signal<any[]>([]);
  clientes = signal<any[]>([]);
  categorias = signal<any[]>([]);
  sucursales = signal<any[]>([]);

  // KPIs computados
  get disponibles() { return this.vehiculos().filter(v => (v.VEH_estado || v.veH_estado) === 'Disponible').length; }
  get reservados() { return this.vehiculos().filter(v => (v.VEH_estado || v.veH_estado) === 'Reservado').length; }
  get pendientes() { return this.reservas().filter(r => (r.RES_estado || r.rES_estado) === 'Pendiente').length; }
  get confirmadas() { return this.reservas().filter(r => (r.RES_estado || r.rES_estado) === 'Confirmada').length; }

  // Reservas recientes (últimas 8)
  get reservasRecientes() {
    return [...this.reservas()]
      .sort((a, b) => new Date(b.RES_fechaCreacion || b.rES_fechaCreacion).getTime()
        - new Date(a.RES_fechaCreacion || a.rES_fechaCreacion).getTime())
      .slice(0, 8);
  }

  // Activity feed
  get activityItems() {
    const events: any[] = [];
    [...this.reservas()]
      .sort((a, b) => new Date(b.RES_fechaCreacion || b.rES_fechaCreacion).getTime()
        - new Date(a.RES_fechaCreacion || a.rES_fechaCreacion).getTime())
      .slice(0, 4)
      .forEach(r => {
        const cliId = r.CLI_id || r.cLI_id;
        const cli = this.clientes().find(c => (c.CLI_id || c.cLI_id) === cliId);
        const nombre = cli ? `${cli.CLI_nombres || cli.cLI_nombres}` : 'Cliente';
        events.push({
          icon: '📋', bg: 'rgba(108,99,255,0.12)',
          text: `<strong>${nombre}</strong> creó una reserva · Estado: ${r.RES_estado || r.rES_estado || 'Pendiente'}`,
          time: this.timeAgo(r.RES_fechaCreacion || r.rES_fechaCreacion)
        });
      });
    this.vehiculos()
      .filter(v => (v.VEH_estado || v.veH_estado) === 'Mantenimiento')
      .slice(0, 2)
      .forEach(v => {
        events.push({
          icon: '🔧', bg: 'rgba(251,191,36,0.1)',
          text: `<strong>${v.VEH_modelo || v.veH_modelo}</strong> (${v.VEH_placa || v.veH_placa}) en mantenimiento`,
          time: 'En curso'
        });
      });
    return events.slice(0, 6);
  }

  // Vehículos por categoría para bar chart
  get categoriaStats() {
    const colors = ['#6c63ff', '#00e5cc', '#fbbf24', '#f87171', '#34d399'];
    const stats = this.categorias().map((cat, i) => ({
      nombre: cat.CAT_nombre || cat.caT_nombre || '—',
      count: this.vehiculos().filter(v => (v.CAT_id || v.caT_id) === (cat.CAT_id || cat.caT_id)).length,
      color: colors[i % colors.length]
    })).sort((a, b) => b.count - a.count);
    const max = Math.max(...stats.map(s => s.count), 1);
    return stats.map(s => ({ ...s, pct: Math.round((s.count / max) * 100) }));
  }

  ngOnInit() {
    this.cargar();
    // Fecha en topbar
    const now = new Date();
    const dias = ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'];
    const meses = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];
    this.lastUpdate.set(`${dias[now.getDay()]}, ${now.getDate()} ${meses[now.getMonth()]} ${now.getFullYear()}`);
  }

  ngAfterViewInit() {
    // El donut se dibuja después de cargar datos
  }

  cargar() {
    this.isRefreshing.set(true);
    const base = `${environment.apiUrl}/api/v1`;

    forkJoin({
      vehiculos: this.http.get<any>(`${base}/vehiculos`),
      reservas: this.http.get<any>(`${base}/reservas`),
      clientes: this.http.get<any>(`${base}/clientes`),
      categorias: this.http.get<any>(`${base}/categorias-vehiculo`),
      sucursales: this.http.get<any>(`${base}/sucursales`),
    }).subscribe({
      next: (res) => {
        this.vehiculos.set(res.vehiculos.Data || res.vehiculos.data || []);
        this.reservas.set(res.reservas.Data || res.reservas.data || []);
        this.clientes.set(res.clientes.Data || res.clientes.data || []);
        this.categorias.set(res.categorias.Data || res.categorias.data || []);
        this.sucursales.set(res.sucursales.Data || res.sucursales.data || []);
        this.isLoading.set(false);
        this.isRefreshing.set(false);
        const t = new Date();
        this.lastUpdate.set(`Actualizado a las ${t.getHours()}:${String(t.getMinutes()).padStart(2, '0')}`);
        setTimeout(() => this.drawDonut(), 50);
      },
      error: (err) => {
        console.error('Error cargando dashboard:', err);
        this.isLoading.set(false);
        this.isRefreshing.set(false);
      }
    });
  }

  drawDonut() {
    const canvas = this.donutCanvas?.nativeElement;
    if (!canvas) return;
    const ctx = canvas.getContext('2d');
    if (!ctx) return;

    const disp = this.disponibles;
    const res = this.reservados;
    const mant = this.vehiculos().filter(v => (v.VEH_estado || v.veH_estado) === 'Mantenimiento').length;
    const total = this.vehiculos().length;

    const segments = [
      { value: disp, color: '#34d399' },
      { value: res, color: '#6c63ff' },
      { value: mant, color: '#fbbf24' },
    ].filter(s => s.value > 0);

    const cx = 70, cy = 70, r = 58, inner = 38;
    ctx.clearRect(0, 0, 140, 140);

    if (!total) {
      ctx.beginPath();
      ctx.arc(cx, cy, r, 0, Math.PI * 2);
      ctx.arc(cx, cy, inner, Math.PI * 2, 0, true);
      ctx.fillStyle = '#1c1c32';
      ctx.fill();
      return;
    }

    let start = -Math.PI / 2;
    segments.forEach(seg => {
      const angle = (seg.value / total) * Math.PI * 2;
      ctx.beginPath();
      ctx.moveTo(cx, cy);
      ctx.arc(cx, cy, r, start, start + angle);
      ctx.arc(cx, cy, inner, start + angle, start, true);
      ctx.closePath();
      ctx.fillStyle = seg.color;
      ctx.fill();
      start += angle;
    });
  }

  getNombreCliente(cliId: string): string {
    const c = this.clientes().find(cl => (cl.CLI_id || cl.cLI_id) === cliId);
    return c ? `${c.CLI_nombres || c.cLI_nombres} ${c.CLI_apellidos || c.cLI_apellidos}` : cliId?.slice(0, 8) + '…';
  }

  getNombreSucursal(sucId: string): string {
    const s = this.sucursales().find(sc => (sc.SUC_id || sc.suC_id) === sucId);
    return s ? (s.SUC_nombre || s.suC_nombre) : '—';
  }

  formatFecha(f: string): string {
    if (!f) return '—';
    return new Date(f).toLocaleDateString('es-EC', { day: '2-digit', month: 'short' });
  }

  timeAgo(f: string): string {
    if (!f) return '';
    const diff = Math.floor((Date.now() - new Date(f).getTime()) / 60000);
    if (diff < 60) return `Hace ${diff}m`;
    if (diff < 1440) return `Hace ${Math.floor(diff / 60)}h`;
    return new Date(f).toLocaleDateString('es-EC');
  }

  estadoBadgeClass(estado: string): string {
    const map: Record<string, string> = {
      'Confirmada': 'est-confirmada',
      'Pendiente': 'est-pendiente',
      'Cancelada': 'est-cancelada',
      'Disponible': 'est-disponible',
      'Reservado': 'est-reservado',
      'Mantenimiento': 'est-mant',
    };
    return map[estado] || 'est-pendiente';
  }

  get donutTotal() { return this.vehiculos().length; }
  get donutDisp() { return this.disponibles; }
  get donutRes() { return this.reservados; }
  get donutMant() { return this.vehiculos().filter(v => (v.VEH_estado || v.veH_estado) === 'Mantenimiento').length; }

  irAReservas() { this.router.navigate(['/admin/reservas']); }
  irAVehiculos() { this.router.navigate(['/admin/vehiculos']); }
}
