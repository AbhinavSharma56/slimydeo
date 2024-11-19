import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HealthMetricService {
  private baseUrl = 'https://localhost:7211/api/HealthMetrics';

  constructor(private http: HttpClient) {}

  // Fetch all metrics
  getAllMetrics(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/metrics`);
  }

  // Fetch health metrics logs by username
  getMetricsLogs(username: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/metrics/logs/${username}`);
  }

  addMetricLog(metricLog: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/metrics/logs`, metricLog);
  }

  deleteMetricLog(logId: number): Observable<any> {
    return this.http.delete<any>(`/api/metrics/logs/${logId}`);
  }
  
}
