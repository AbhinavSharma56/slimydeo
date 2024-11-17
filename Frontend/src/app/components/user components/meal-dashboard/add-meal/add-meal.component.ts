import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MealService } from '../../../../services/meal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-meal',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-meal.component.html',
  styleUrls: ['./add-meal.component.css'], // Corrected property name
})
export class AddMealComponent {
  meal = { mealId: 0, username: '', mealType: '', consumptionDate: '' };
  foodDetails: any[] = [];
  foodItemsCount = 1;

  // Meal type options
  mealTypes = [
    { value: 'BREAKFAST', display: 'Breakfast' },
    { value: 'SNACK (MORNING)', display: 'Snack (Morning)' },
    { value: 'LUNCH', display: 'Lunch' },
    { value: 'SNACK (EVENING)', display: 'Snack (Evening)' },
    { value: 'DINNER', display: 'Dinner' },
  ];

  constructor(
    private mealService: MealService,
    private toastr: ToastrService
  ) {}

  updateFoodItemsCount(): void {
    this.foodDetails = Array(this.foodItemsCount).fill({
      foodId: 0,
      foodName: '',
      quantity: 0,
      unit: '',
    });
  }

  addMeal(): void {
    if (localStorage.getItem('loggedUser') !== null) {
      this.meal.username = localStorage.getItem('loggedUser')!;
    }
    this.mealService.addMeal(this.meal).subscribe({
      next: (response) => {
        if (response.success) {
          const mealId = response.data.mealId;
          this.addFoodDetails(mealId);
        }
      },
      error: () => this.toastr.error('Failed to add meal.'),
    });
  }

  addFoodDetails(mealId: number): void {
    this.mealService.addFoodDetailsBulk(mealId, this.foodDetails).subscribe({
      next: (response) => {
        if (response.success) {
          this.toastr.success('Meal and foods added successfully.');
          this.resetForm();
        }
      },
      error: () => this.toastr.error('Failed to add food details.'),
    });
  }

  resetForm(): void {
    this.meal = { mealId: 0, username: '', mealType: '', consumptionDate: '' };
    this.foodDetails = [];
    this.foodItemsCount = 1;
    this.updateFoodItemsCount();
  }
}
