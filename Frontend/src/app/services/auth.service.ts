import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private baseUrl = 'https://localhost:7014/api/AuthService';

  constructor(private http: HttpClient) {}

  registerUser(registerObj: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Register`, registerObj); // Correct usage of baseUrl
  }
  
  loginUser(loginObj: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Login`, loginObj); // Use baseUrl here for consistency
  }
}