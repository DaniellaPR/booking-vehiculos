import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const token = authService.getToken();

  if (!token) {
    router.navigate(['/login']);
    return false;
  }

  try {
    // Decodificar el Payload del JWT
    const payload = JSON.parse(atob(token.split('.')[1]));
    // .NET guarda los roles bajo esta key genérica por defecto:
    const userRole = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    const expectedRoles = route.data['roles'] as Array<string>;

    // Verificamos si el rol del usuario está permitido en esta ruta
    if (expectedRoles && expectedRoles.includes(userRole)) {
      return true;
    }
  } catch (e) {
    console.error('Error al leer el token');
  }

  // Si no tiene permisos, lo devolvemos al inicio público
  router.navigate(['/']);
  return false;
};
