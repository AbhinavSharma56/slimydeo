import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MoodService {
  private apiUrl = 'https://localhost:7283/api/Mood';

  constructor(private http: HttpClient) { }

  getMoods(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl); // Adjust type according to API response
  }
}
