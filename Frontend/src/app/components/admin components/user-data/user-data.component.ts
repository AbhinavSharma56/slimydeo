import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';

@Component({
  selector: 'app-user-data',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-data.component.html',
  styleUrl: './user-data.component.css'
})
export class UserDataComponent {
  userProfiles: {
    userProfileId: number;
    username: string;
    fullName: string;
    dob: string;
    email: string;
    mobileNumber: number;
    gender: string;
    address: string;
    profilePicture: string;
    createdAt: string;
    weight: number;
    height: number;
  }[] = []; // Inline interface for user profiles

  errorMessage: string | null = null; // To display error messages if any

  constructor(private http: HttpClient, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.fetchUserProfiles();
  }

  fetchUserProfiles(): void {
    this.http
      .get<{ data: any }>('https://localhost:7006/api/UserProfile/AllUsers')
      .subscribe({
        next: (response) => {
          if (response && response.data) {
            this.userProfiles = response.data;
          } else {
            this.userProfiles = [];
          }
          this.cd.detectChanges(); // Trigger change detection
        },
        error: (err) => {
          console.error('Error fetching user profiles', err);
          this.errorMessage = 'Failed to load user profiles.';
        }
      });
  }
}
