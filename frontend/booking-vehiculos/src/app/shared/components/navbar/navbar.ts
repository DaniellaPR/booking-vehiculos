import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service'; // Ajusta la ruta a tu auth.service

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.scss']
})
export class NavbarComponent {
  authService = inject(AuthService);
  router = inject(Router);

  // Observable o signal que indica si está logueado
  isLoggedIn$ = this.authService.isLoggedIn$;

  cerrarSesion() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
