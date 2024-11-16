import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-user-navbar',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './user-navbar.component.html',
  styleUrl: './user-navbar.component.css',
})
export class UserNavbarComponent {
  router = inject(Router);

  logout(): void {
    // Clear user data from localStorage or any session management
    localStorage.removeItem('loggedUser');
    localStorage.removeItem('loggedUserRole');
    localStorage.removeItem('loggedUserToken');

    // Navigate to login page
    this.router.navigate(['/']);
  }
}
