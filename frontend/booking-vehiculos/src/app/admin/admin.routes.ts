import { Routes } from '@angular/router';

export const adminRoutes: Routes = [
  {
    path: '',
    children: [
      { path: 'dashboard', loadComponent: () => import('./dashboard/dashboard').then(m => m.DashboardComponent) },
      { path: 'vehiculos', loadComponent: () => import('./vehiculos/vehiculos').then(m => m.VehiculosComponent) },
      { path: 'sucursales', loadComponent: () => import('./sucursales/sucursales').then(m => m.SucursalesComponent) },
      { path: 'reservas', loadComponent: () => import('./reservas/reservas').then(m => m.ReservasComponent) },
      { path: 'clientes', loadComponent: () => import('./clientes/clientes').then(m => m.ClientesComponent) },
      {
        path: 'catalogo',
        children: [
          { path: 'categorias', loadComponent: () => import('./catalogo/categorias/categorias').then(m => m.CategoriasComponent) },
          { path: 'seguros', loadComponent: () => import('./catalogo/seguros/seguros').then(m => m.SegurosComponent) },
          { path: 'extras', loadComponent: () => import('./catalogo/extras/extras').then(m => m.ExtrasComponent) },
          { path: 'tarifas', loadComponent: () => import('./catalogo/tarifas/tarifas').then(m => m.TarifasComponent) },
        ]
      },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];
