import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

export const loginGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  // Check if the user is logged in
  const user = localStorage.getItem('loggedUser');
  const userRole = localStorage.getItem('loggedUserRole');

  if (user) {
    // Redirect based on user role
    if (userRole === 'ADMIN') {
      router.navigate(['/admin']);
    } else if (userRole === 'USER') {
      router.navigate(['/user']);
    }
    return false; // Prevent navigation to the login page
  }

  return true; // Allow access to the login page if not logged in
};