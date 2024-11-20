import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AddMetricLogComponent } from '../add-metric-log/add-metric-log.component';
import { UpdateMetricLogComponent } from '../update-metric-log/update-metric-log.component';
import { DeleteMetricLogComponent } from '../delete-metric-log/delete-metric-log.component';
import { ToastrService } from 'ngx-toastr';
import { HealthMetricService } from '../../../../services/health-metric.service';

@Component({
  selector: 'app-list-metric-log',
  standalone: true,
  imports: [CommonModule, AddMetricLogComponent, DeleteMetricLogComponent],
  templateUrl: './list-metric-log.component.html',
  styleUrl: './list-metric-log.component.css',
})
export class ListMetricLogComponent {
  healthMetricsLogs: any[] = []; // Health metrics logs for the user
  metricsList: any[] = []; // Complete list of metrics
  loading: boolean = false; // Loading state
  selectedLog: any = null; // Log selected for editing or deleting
  editing = false; // Whether the edit mode is active
  deleting = false; // Whether the delete mode is active

  constructor(
    private healthMetricsService: HealthMetricService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadMetricsList();
  }

  // Load the list of available metrics
  loadMetricsList(): void {
    this.loading = true;
    this.healthMetricsService.getAllMetrics().subscribe({
      next: (response) => {
        if (response.success) {
          this.metricsList = response.data;
          this.loadMetricsLogs(); // Load logs after metrics are available
        } else {
          this.toastr.error('Failed to load metrics');
        }
      },
      error: () => this.toastr.error('Error fetching metrics'),
      complete: () => (this.loading = false),
    });
  }
  username = localStorage.getItem('loggedUser');
  // Load the health metrics logs for the user
  loadMetricsLogs(): void {
    this.loading = true;
    this.healthMetricsService.getMetricsLogs(this.username!).subscribe({
      next: (response) => {
        if (response.success) {
          this.loadMetricsList();
          this.healthMetricsLogs = response.data
            .map((log: any) => {
              const metric = this.metricsList.find(
                (metric) => metric.metricId === log.metricId
              );
              return {
                ...log,
                metricName: metric?.metricName || 'Unknown',
                metricUnit: metric?.unit || '',
                metricDescription: metric?.description || '',
              };
            })
            .sort(
              (a: any, b: any) =>
                new Date(b.dateRecorded).getTime() -
                new Date(a.dateRecorded).getTime()
            );
        } else {
          this.toastr.error(response.message || 'Failed to load logs');
        }
      },
      error: () => this.toastr.error('Error fetching health metrics logs'),
      complete: () => (this.loading = false),
    });
    this.loading = false;
  }

  // Edit action
  onEdit(log: any): void {
    this.editing = true;
    this.selectedLog = log;
    console.log('deleting:', this.deleting);
    console.log('selectedLog:', this.selectedLog);
  }

  // Cancel editing
  cancelEdit(): void {
    this.editing = false;
    this.selectedLog = null;
  }

  // Delete action
  onDelete(log: any): void {
    console.log(log);
    this.deleting = true;
    this.selectedLog = log;
  }

  // Cancel deletion
  cancelDelete(): void {
    this.deleting = false;
    this.selectedLog = null;
  }

  // Refresh the list
  refreshList(): void {
    this.cancelEdit();
    this.cancelDelete();
    this.loadMetricsLogs();
  }
}
