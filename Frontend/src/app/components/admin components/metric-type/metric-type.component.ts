import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-metric-type',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './metric-type.component.html',
  styleUrl: './metric-type.component.css',
})
export class MetricTypeComponent {
  // Metric object to hold form data
  metricObj = {
    metricId: 0,
    metricName: '',
    unit: '',
    description: '',
    createdBy: '',
    createdAt: '',
  };

  // Array to hold fetched metrics
  submittedMetrics: any[] = [];
  isSubmitting = false; // Track the submission status
  isEditing = false; // Track if the form is in edit mode

  constructor(private http: HttpClient, private cd: ChangeDetectorRef) {}

  // Function to handle the form submission
  onSubmit(): void {
    this.isSubmitting = true;

    if (this.metricObj.metricId === 0) {
      // POST request for new metric
      this.http
        .post('https://localhost:7211/api/HealthMetrics/metrics', this.metricObj)
        .subscribe((res: any) => {
          if (res.metricId >= 0) {
            alert('Metric Record Created!');
            this.submittedMetrics.push(res); // Add new metric to the list
          } else {
            alert('Problem in Metric Creation');
          }
          this.isSubmitting = false;
          this.resetForm();
          this.cd.detectChanges();
        });
    } else {
      // PUT request to update metric
      this.http
        .put(
          `https://localhost:7211/api/HealthMetrics/metrics/${this.metricObj.metricId}`,
          this.metricObj
        )
        .subscribe((res: any) => {
          if (res.metricId >= 0) {
            alert('Metric Record Updated!');
            const index = this.submittedMetrics.findIndex(
              (metric) => metric.metricId === this.metricObj.metricId
            );
            if (index !== -1) {
              this.submittedMetrics[index] = { ...this.metricObj };
            }
          } else {
            alert('Problem in Metric Update');
          }
          this.isSubmitting = false;
          this.resetForm();
          this.cd.detectChanges();
        });
    }
  }

  // Fetch metrics from backend
  fetchMetricLogs(): void {
    this.http
      .get<any[]>('https://localhost:7211/api/HealthMetrics/metrics')
      .subscribe((data) => {
        this.submittedMetrics = data;
        this.cd.detectChanges();
      });
  }

  // Edit metric log
  editMetric(metric: any): void {
    this.metricObj = { ...metric }; // Pre-fill form with metric data
    this.isEditing = true;
  }

  // Reset the form
  resetForm(): void {
    this.metricObj = {
      metricId: 0,
      metricName: '',
      unit: '',
      description: '',
      createdBy: '',
      createdAt: '',
    };
    this.isEditing = false;
  }

  // Initialize component
  ngOnInit(): void {
    this.fetchMetricLogs();
  }
}
