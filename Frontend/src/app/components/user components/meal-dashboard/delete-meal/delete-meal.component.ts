import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { MealService } from '../../../../services/meal.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete-meal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './delete-meal.component.html',
  styleUrls: ['./delete-meal.component.css'],
})
export class DeleteMealComponent {
  @Input() meal: any; // Meal object passed from the parent
  @Output() close = new EventEmitter<void>();
  @Output() success = new EventEmitter<void>();

  constructor(
    private mealService: MealService,
    private toastr: ToastrService
  ) {}

  onDelete(): void {
    if (!this.meal || !this.meal.mealId) {
      this.toastr.error('Invalid meal details provided.');
      return;
    }

    this.mealService.deleteMeal(this.meal.mealId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.toastr.success(response.message || 'Meal deleted successfully.');
          this.success.emit(); // Notify parent about successful deletion
          this.close.emit(); // Close the delete form
        } else {
          this.toastr.error(response.message || 'Failed to delete the meal.');
        }
      },
      error: () => {
        this.toastr.error('An error occurred while deleting the meal.');
      },
    });
  }
}
