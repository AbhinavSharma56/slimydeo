import { Component, EventEmitter, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HealthMetricService } from '../../../../services/health-metric.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-metric-log',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-metric-log.component.html',
  styleUrl: './add-metric-log.component.css',
})
export class AddMetricLogComponent {
  @Output() success = new EventEmitter<void>();
  loading: boolean = false; // Loading state
  metricsList: any[] = []; // Complete list of metrics

  healthMetricLog = {
    logId: 0,
    username: localStorage.getItem('loggedUser'),
    metricId: 0,
    value: 0,
    dateRecorded: this.getCurrentDateTime(),
  };

  constructor(
    private healthMetricsService: HealthMetricService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadMetricsList(); // Load metrics when component initializes
  }

  // Helper method to get current date and time in 'YYYY-MM-DDTHH:mm' format
  getCurrentDateTime(): string {
    const now = new Date();
    return now.toISOString().slice(0, 16); // Format for datetime-local input
  }

  loadMetricsList(): void {
    this.loading = true; // Start the loading spinner
    this.healthMetricsService.getAllMetrics().subscribe({
      next: (response) => {
        if (response.success) {
          this.metricsList = response.data; // Save fetched metrics
        } else {
          this.toastr.error('Failed to load metrics');
        }
      },
      complete: () => (this.loading = false), // Stop loading spinner
    });
  }

  addHealthMetricLog(): void {
    if (this.healthMetricLog.username) {
      if (this.healthMetricLog.metricId) {
        if (this.healthMetricLog.value != 0) {
          this.healthMetricsService
            .addMetricLog(this.healthMetricLog)
            .subscribe({
              next: (response) => {
                if (response.success) {
                  this.toastr.success('Health Metric Log added successfully.');
                  this.success.emit();
                  this.resetForm();
                } else {
                  this.toastr.error(
                    response.message || 'Failed to add metric log.'
                  );
                }
              },
              error: () => this.toastr.error('Failed to add metric log.'),
            });
        } else {
          this.toastr.error('Please enter a value.');
        }
      } else {
        this.toastr.error('Please select a Metric.');
      }
    } else {
      this.toastr.error('User not logged in.');
    }
  }

  resetForm(): void {
    this.healthMetricLog = {
      logId: 0,
      username: localStorage.getItem('loggedUser'),
      metricId: 0,
      value: 0,
      dateRecorded: this.getCurrentDateTime(),
    };
  }
}
