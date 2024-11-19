import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HealthMetricService } from '../../../../services/health-metric.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-delete-metric-log',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './delete-metric-log.component.html',
  styleUrl: './delete-metric-log.component.css'
})
export class DeleteMetricLogComponent {
  @Input() log: any; // Health metric log object passed from the parent
  @Output() close = new EventEmitter<void>();
  @Output() success = new EventEmitter<void>();

  constructor(
    private healthMetricService: HealthMetricService,
    private toastr: ToastrService
  ) {}

  onDelete(): void {
    if (!this.log || !this.log.logId) {
      this.toastr.error('Invalid metric log details provided.');
      return;
    }

    this.healthMetricService.deleteMetricLog(this.log.logId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.toastr.success(
            response.message || 'Metric log deleted successfully.'
          );
          this.success.emit(); // Notify parent about successful deletion
          this.close.emit(); // Close the delete form
        } else {
          this.toastr.error(
            response.message || 'Failed to delete the metric log.'
          );
        }
      },
      error: () => {
        this.toastr.error('An error occurred while deleting the metric log.');
      },
    });
  }
}