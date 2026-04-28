import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { BASE_PATH } from './core/api'; // Tu carpeta de API generada
import { environment } from '../environments/environment'; // Importa el environment

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    // CAMBIO AQUÍ: Ahora usa la URL del environment, no localhost
    { provide: BASE_PATH, useValue: environment.apiUrl }
  ]
};
