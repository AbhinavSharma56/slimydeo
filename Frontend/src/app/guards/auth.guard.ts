import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  // Retrieve the user object from localStorage
  const user = localStorage.getItem('loggedUser');
  const userRole = localStorage.getItem('loggedUserRole');
  const requestedUrl = route.url.join('/'); // Join the UrlSegment array to a string

  // Check if the user is logged in (you can also check if the token exists)
  if (!user) {
    // If the user is not logged in, redirect to the login page
    router.navigateByUrl('login');
    return false;
  }

  // Check if the user is trying to access the wrong dashboard based on their role
  if (requestedUrl.includes('admin') && userRole !== 'ADMIN') {
    // If the user is not an admin and tries to access the admin dashboard
    router.navigate(['/user']); // Redirect to user dashboard
    return false;
  }

  if (requestedUrl.includes('user') && userRole !== 'USER') {
    // If the user is not a regular user and tries to access the user dashboard
    router.navigate(['/admin']); // Redirect to admin dashboard
    return false;
  }

  // If everything is okay (logged in and accessing the right dashboard)
  return true;
};
