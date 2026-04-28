import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../auth/auth.service';

// Rutas siempre públicas (nunca llevan token)
const ALWAYS_PUBLIC = [
  '/api/v1/auth/login',
  '/api/v1/auth/registro',
];

// Rutas públicas solo para POST sin sesión activa (registro de cliente)
const PUBLIC_POST_ANONYMOUS = [
  '/api/v1/clientes',
  '/api/v1/usuarios-app',
];

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);

  // 1. Rutas siempre públicas → nunca adjuntar token
  if (ALWAYS_PUBLIC.some(r => req.url.includes(r))) {
    return next(req);
  }

  const token = auth.getToken();

  // 2. POST a rutas de registro sin sesión activa → dejar pasar sin token
  //    (registro de nuevo cliente crea tanto Cliente como UsuarioApp públicamente)
  if (req.method === 'POST' && !token && PUBLIC_POST_ANONYMOUS.some(r => req.url.includes(r))) {
    return next(req);
  }

  // 3. Resto: adjuntar token si existe
  if (token) {
    return next(req.clone({ setHeaders: { Authorization: `Bearer ${token}` } }));
  }

  return next(req);
};
