import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserProfileService } from '../../../services/user-profile.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css',
})
export class UserProfileComponent {
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

  isSubmitting = false;

  constructor(private userProfileService: UserProfileService) {}

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

  onSubmit() {
    this.isSubmitting = true;
    this.userProfileService.updateUserProfile(this.userProfileObj).subscribe(
      (response) => {
        console.log('Profile updated successfully:', response);
        this.isSubmitting = false;
      },
      (error) => {
        console.error('Error updating profile:', error);
        this.isSubmitting = false;
      }
    );
  }

  resetForm() {
    this.ngOnInit(); // Reload user data from the server
  }
}