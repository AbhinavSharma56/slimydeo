import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  private apiUrl = 'https://localhost:7006/api/UserProfile';

  constructor(private http: HttpClient) {}

  // Fetch user profile by username
  getUserProfile(username: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${username}`);
  }

  // Update user profile
  updateUserProfile(userProfile: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${userProfile.username}`, userProfile);
  }
}
