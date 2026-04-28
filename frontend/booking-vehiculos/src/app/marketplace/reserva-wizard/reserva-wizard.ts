import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';

import {
  SucursalesService,
  SegurosService,
  ExtraAdicionalesService,
  CategoriasVehiculoService,
  VehiculosService,
  TarifasService,
  ReservasService
} from '../.././core/api';
import { CrearReservaRequest } from '../../core/api/model/crearReservaRequest';

@Component({
  selector: 'app-reserva-wizard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reserva-wizard.html',
  styleUrls: ['./reserva-wizard.scss']
})
export class ReservaWizardComponent implements OnInit {
  public router = inject(Router);
  private route = inject(ActivatedRoute);
  public authService = inject(AuthService);

  private sucursalesSvc = inject(SucursalesService);
  private segurosSvc = inject(SegurosService);
  private extrasSvc = inject(ExtraAdicionalesService);
  private vehiculosSvc = inject(VehiculosService);
  private categoriasSvc = inject(CategoriasVehiculoService);
  private tarifasSvc = inject(TarifasService);
  private reservasSvc = inject(ReservasService);

  // Control de pasos
  step = signal<number>(1);
  isLoading = signal<boolean>(false);
  errorMsg = signal<string>('');
  fechaMinima: string = '';

  // Datos reales de la BD
  vehiculoSeleccionado = signal<any>(null);
  sucursales = signal<any[]>([]);
  seguros = signal<any[]>([]);
  extras = signal<any[]>([]);
  categorias = signal<any[]>([]);
  tarifas = signal<any[]>([]);

  reservaData = signal({
    fechaRetiro: '',
    fechaDevolucion: '',
    sucursalRetiroId: '',
    sucursalDevolucionId: '',
    seguroId: '',
    extrasSeleccionados: [] as any[],
  });

  reservaExitosa = signal<boolean>(false);

  // ── Computed: precio base desde TARIFA (tabla real) ────────────────────
  precioBaseVehiculo = computed(() => {
    const v = this.vehiculoSeleccionado();
    if (!v) return 0;
    const catId = v.CAT_id || v.caT_id;
    const tarifa = this.tarifas().find(t => (t.CAT_id || t.caT_id) === catId);
    if (tarifa) return tarifa.TAR_precioDiario || tarifa.taR_precioDiario || 0;
    // fallback en categoría
    const cat = this.categorias().find(c => (c.CAT_id || c.caT_id) === catId);
    return cat ? (cat.CAT_costoBase || cat.caT_costoBase || 0) : 0;
  });

  // ── Computed: número de días ────────────────────────────────────────────
  numeroDias = computed(() => {
    const { fechaRetiro, fechaDevolucion } = this.reservaData();
    if (!fechaRetiro || !fechaDevolucion) return 1;
    const ms = new Date(fechaDevolucion).getTime() - new Date(fechaRetiro).getTime();
    const dias = Math.ceil(ms / (1000 * 60 * 60 * 24));
    return dias > 0 ? dias : 1;
  });

  // ── Computed: costo seguro por día ──────────────────────────────────────
  costoSeguroDiario = computed(() => {
    const seguroId = this.reservaData().seguroId;
    if (!seguroId) return 0;
    const seg = this.seguros().find(s => (s.SEG_id || s.seG_id) === seguroId);
    return seg ? (seg.SEG_costoDiario || seg.seG_costoDiario || 0) : 0;
  });

  // ── Computed: extras total (por día) ───────────────────────────────────
  totalExtras = computed(() =>
    this.reservaData().extrasSeleccionados
      .reduce((sum: number, ext: any) => sum + (ext.EXT_costo || ext.exT_costo || 0), 0)
  );

  // ── Computed: total diario ──────────────────────────────────────────────
  costoDiario = computed(() =>
    this.precioBaseVehiculo() + this.costoSeguroDiario() + this.totalExtras()
  );

  // ── Computed: TOTAL FINAL (días × diario) ──────────────────────────────
  costoTotal = computed(() => this.costoDiario() * this.numeroDias());

  ngOnInit() {
    const hoy = new Date();
    const manana = new Date(hoy);
    manana.setDate(manana.getDate() + 1);
    this.fechaMinima = hoy.toISOString().split('T')[0];

    this.reservaData.update(d => ({
      ...d,
      fechaRetiro: this.fechaMinima,
      fechaDevolucion: manana.toISOString().split('T')[0]
    }));

    this.cargarCatalogos();

    // ⚠️ La ruta es ':vehiculoId', no ':id'
    const vehiculoId = this.route.snapshot.paramMap.get('vehiculoId');
    if (vehiculoId) {
      this.obtenerVehiculo(vehiculoId);
    }
  }

  cargarCatalogos() {
    this.categoriasSvc.apiV1CategoriasVehiculoGet().subscribe({
      next: (res: any) => this.categorias.set(res.Data || res.data || [])
    });
    this.tarifasSvc.apiV1TarifasGet().subscribe({
      next: (res: any) => this.tarifas.set(res.Data || res.data || [])
    });
    this.sucursalesSvc.apiV1SucursalesGet().subscribe({
      next: (res: any) => this.sucursales.set(res.Data || res.data || []),
      error: (err) => console.error('Error cargando sucursales:', err)
    });
    this.segurosSvc.apiV1SegurosGet().subscribe({
      next: (res: any) => this.seguros.set(res.Data || res.data || []),
      error: (err) => console.error('Error cargando seguros:', err)
    });
    this.extrasSvc.apiV1ExtrasAdicionalesGet().subscribe({
      next: (res: any) => this.extras.set(res.Data || res.data || []),
      error: (err) => console.error('Error cargando extras:', err)
    });
  }

  obtenerVehiculo(id: string) {
    this.vehiculosSvc.apiV1VehiculosIdGet(id).subscribe({
      next: (res: any) => this.vehiculoSeleccionado.set(res.Data || res.data || res),
      error: (err) => console.error('Error cargando vehículo:', err)
    });
  }

  isStep1Valid(): boolean {
    const d = this.reservaData();
    return !!d.fechaRetiro && !!d.fechaDevolucion &&
      !!d.sucursalRetiroId && !!d.sucursalDevolucionId;
  }

  nextStep() {
    if (this.step() === 1 && !this.isStep1Valid()) {
      alert('⚠️ Selecciona fechas y sucursales para continuar.');
      return;
    }
    if (this.step() < 4) this.step.update(s => s + 1);
  }

  prevStep() {
    if (this.step() > 1) this.step.update(s => s - 1);
  }

  toggleExtra(extra: any) {
    const data = this.reservaData();
    const id = extra.EXT_id || extra.exT_id;
    const idx = data.extrasSeleccionados.findIndex((e: any) => (e.EXT_id || e.exT_id) === id);
    if (idx > -1) data.extrasSeleccionados.splice(idx, 1);
    else data.extrasSeleccionados.push(extra);
    this.reservaData.set({ ...data });
  }

  isExtraSelected(extra: any): boolean {
    const id = extra.EXT_id || extra.exT_id;
    return this.reservaData().extrasSeleccionados.some((e: any) => (e.EXT_id || e.exT_id) === id);
  }

  setFechaRetiro(val: string) {
    this.reservaData.update(d => ({ ...d, fechaRetiro: val }));
  }

  setFechaDevolucion(val: string) {
    this.reservaData.update(d => ({ ...d, fechaDevolucion: val }));
  }

  setSucursalRetiro(val: string) {
    this.reservaData.update(d => ({ ...d, sucursalRetiroId: val }));
  }

  setSucursalDevolucion(val: string) {
    this.reservaData.update(d => ({ ...d, sucursalDevolucionId: val }));
  }

  setSeguro(val: string) {
    this.reservaData.update(d => ({ ...d, seguroId: val }));
  }


  // ── Confirmación real contra la API ────────────────────────────────────
  finalizarReserva() {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    this.isLoading.set(true);
    this.errorMsg.set('');

    const d = this.reservaData();
    const clienteId = this.authService.getClienteId();

    const payload: CrearReservaRequest = {
      CLI_id: clienteId ?? undefined,
      RES_sucursalRetiroId: d.sucursalRetiroId,
      RES_sucursalEntregaId: d.sucursalDevolucionId,
      RES_fechaRetiro: new Date(d.fechaRetiro).toISOString(),
      RES_fechaEntrega: new Date(d.fechaDevolucion).toISOString(),
      RES_estado: 'Pendiente',
      RES_usuarioCreacion: this.authService.getEmail() || 'cliente'
    };

    this.reservasSvc.apiV1ReservasPost(payload).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.reservaExitosa.set(true);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.errorMsg.set('No se pudo guardar la reserva. Intenta de nuevo.');
        console.error('Error al crear reserva:', err);
      }
    });
  }

  irAInicio() { this.router.navigate(['/']); }
  irAMisReservas() { this.router.navigate(['/mis-reservas']); }
  irALogin() { this.router.navigate(['/login']); }
}
