import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'https://localhost:7006/api/UserProfile';

  constructor(private http: HttpClient) { }

  registerUserDetails(registerDetailsObj: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/AddUserProfile`, registerDetailsObj);
  }
}