import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-health-dashboard',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterOutlet],
  templateUrl: './health-dashboard.component.html',
  styleUrl: './health-dashboard.component.css'
})
export class HealthDashboardComponent {
  healthMetricsObj = {
    logId: 0,
    username: '',
    metricId: 0,
    value: 0,
    dateRecorded: ''
  };

  submittedLogs: any[] = [];
  metrics: any[] = []; // To store metrics fetched from the backend
  isSubmitting = false;
  isEditing = false;

  constructor(private http: HttpClient, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.fetchMetrics();
    this.fetchHealthMetricsLogs();
  }

  fetchMetrics(): void {
    this.http.get<any[]>('https://localhost:7211/api/HealthMetrics/metrics')
      .subscribe(data => {
        this.metrics = data; // Store the metrics data
        this.cd.detectChanges();
      });
  }

  fetchHealthMetricsLogs(): void {
    this.http.get<any[]>('https://localhost:7211/api/HealthMetrics/metrics/logs')
      .subscribe(data => {
        this.submittedLogs = data;
        this.cd.detectChanges();
      });
  }

  onSubmit(): void {
    // Validate if metricId is properly selected
    if (!this.healthMetricsObj.metricId || this.healthMetricsObj.metricId <= 0) {
      alert('Please select a valid metric from the dropdown.');
      return;
    }

    if (localStorage.getItem('loggedUser') !== null) {
      this.healthMetricsObj.username = localStorage.getItem('loggedUser')!;
    }

    this.isSubmitting = true;

    this.http.post("https://localhost:7211/api/HealthMetrics/metrics/logs", this.healthMetricsObj)
      .subscribe((res: any) => {
        if (res.logId >= 0) {
          alert("Health Metric Log Created!");
          this.submittedLogs.push(res); // Add the new log
        } else {
          alert("Problem in Creating Log");
        }
        this.isSubmitting = false;
        this.resetForm();
        this.cd.detectChanges();
      });
  }

  resetForm(): void {
    this.healthMetricsObj = {
      logId: 0,
      username: '',
      metricId: 0,
      value: 0,
      dateRecorded: ''
    };
    this.isEditing = false;
  }

  // Helper function to get metric name by ID
  getMetricName(metricId: number): string {
    const metric = this.metrics.find(m => m.metricId === metricId);
    return metric ? metric.metricName : 'Unknown Metric';
  }
}
