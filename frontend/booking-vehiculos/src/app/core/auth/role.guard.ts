// app/app/core/auth/role.guard.ts
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.getToken()) {
    router.navigate(['/login']);
    return false;
  }

  const expectedRoles: string[] = route.data['roles'] ?? [];
  if (expectedRoles.length === 0) return true;

  // Usamos AuthService (ya normaliza los roles a ADMIN / VENDEDOR / CLIENTE)
  const userRoles = authService.getRoles();
  const allowed = expectedRoles.some(r => userRoles.includes(r));

  if (allowed) return true;

  router.navigate(['/']);
  return false;
};
