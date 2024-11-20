import { Component, OnInit, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { UserProfileService } from '../../../services/user-profile.service';

@Component({
  selector: 'app-user-navbar',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './user-navbar.component.html',
  styleUrl: './user-navbar.component.css',
})
export class UserNavbarComponent implements OnInit {
  userProfileObj = {
    username: '',
    fullName: '',
    dob: '',
    email: '',
    mobileNumber: '',
    gender: '',
    address: '',
    weight: null,
    height: null,
  };
  
  router = inject(Router);

  ngOnInit(): void {
    const username = localStorage.getItem('loggedUser');
    if (username) {
      this.userProfileService.getUserProfile(username).subscribe(
        (response) => {
          this.userProfileObj = response.data;
          console.log(this.userProfileObj);
        },
        (error) => {
          console.error('Error fetching user profile:', error);
        }
      );
    }
  }

  constructor(private userProfileService: UserProfileService) {}

  logout(): void {
    // Clear user data from localStorage or any session management
    localStorage.removeItem('loggedUser');
    localStorage.removeItem('loggedUserRole');
    localStorage.removeItem('loggedUserToken');

    // Navigate to login page
    this.router.navigate(['/']);
  }
}
