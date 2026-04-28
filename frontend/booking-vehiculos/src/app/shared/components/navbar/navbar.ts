import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.scss']
})
export class NavbarComponent {
  public authService = inject(AuthService);
  public router = inject(Router);

  goToLogin() {
    this.router.navigate(['/login']);
  }

  // Agrega este nuevo método:
  goToRegistro() {
    this.router.navigate(['/registro']);
  }

  goToDashboard() {
    this.router.navigate(['/admin/dashboard']);
  }

  logout() {
    // 1. Limpieza silenciosa y manual (para evitar que un service dispare un window.location.reload)
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    sessionStorage.clear();

    // 2. Navegación pura de Angular destruyendo el historial actual (sin recargar la pestaña)
    this.router.navigateByUrl('/login', { replaceUrl: true }).then(() => {
      // Solo recargamos el estado de auth internamente
      this.authService.logout();
    });
  }
}
