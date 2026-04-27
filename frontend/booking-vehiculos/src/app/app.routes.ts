import { Routes } from '@angular/router';
import { authGuard } from './core/auth/auth.guard';
import { roleGuard } from './core/auth/role.guard';

export const routes: Routes = [
  // ── MARKETPLACE PÚBLICO (sin token requerido) ────────────────────────────
  {
    path: '',
    loadComponent: () => import('./marketplace/landing/landing').then(m => m.LandingComponent)
  },
  {
    path: 'buscar',
    loadComponent: () => import('./marketplace/busqueda/busqueda').then(m => m.BusquedaComponent)
  },
  {
    path: 'vehiculo/:id',
    loadComponent: () => import('./marketplace/vehiculo-detalle/vehiculo-detalle').then(m => m.VehiculoDetalleComponent)
  },

  // ── AUTH ────────────────────────────────────────────────────────────────
  {
    path: 'login',
    loadComponent: () => import('./auth/login/login').then(m => m.LoginComponent)
  },
  {
    path: 'registro',
    loadComponent: () => import('./auth/registro/registro').then(m => m.RegistroComponent)
  },

  // ── RESERVA: requiere estar autenticado (cualquier rol) ─────────────────
  // Si no está logueado → lo manda al login y luego vuelve aquí
  {
    path: 'reservar/:vehiculoId',
    canActivate: [authGuard],
    loadComponent: () => import('./marketplace/reserva-wizard/reserva-wizard').then(m => m.ReservaWizardComponent)
  },

  // ── PANEL ADMIN: requiere rol ADMIN o VENDEDOR ──────────────────────────
  {
    path: 'admin',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['ADMIN', 'VENDEDOR'] },
    loadComponent: () => import('./admin/layout/layout').then(m => m.LayoutComponent),
    loadChildren: () => import('./admin/admin.routes').then(m => m.adminRoutes)
  },

  { path: '**', redirectTo: '' }
];
