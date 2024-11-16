import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-admin-navbar',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './admin-navbar.component.html',
  styleUrl: './admin-navbar.component.css'
})
export class AdminNavbarComponent {
  router = inject(Router);

  logout(): void {
    // Clear user data from localStorage or any session management
    localStorage.removeItem('loggedUser');
    localStorage.removeItem('loggedUserRole');
    localStorage.removeItem('loggedUserToken');

    // Navigate to home page
    this.router.navigate(['/']);
  }
}
