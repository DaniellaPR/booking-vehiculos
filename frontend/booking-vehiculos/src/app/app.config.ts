import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
// 1. Importa el BASE_PATH de tu carpeta api generada
import { BASE_PATH } from './core/api';
// 2. Importa tu environment donde tienes apiUrl: 'https://localhost:7291/api/v1'
import { environment } from '../environments/environment';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    { provide: BASE_PATH, useValue: 'https://localhost:7291' }
  ]
};
