import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { MealService } from '../../../../services/meal.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-meal-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './meal-details.component.html',
  styleUrl: './meal-details.component.css'
})
export class MealDetailsComponent implements OnChanges{
  @Input() meal: any; // Receive the meal object
  @Output() close = new EventEmitter<void>(); // Emit event to close the details view
  foodDetails: any[] = []; // Store fetched food details
  loading = false; // Handle loading state
  error: string | null = null; // Handle errors

  constructor(private mealService: MealService, private toastrService: ToastrService) {} // Inject the MealService

  ngOnInit(): void {
    if (this.meal?.foods?.length > 0) {
      const foodIds = this.meal.foods.map((food: any) => food.foodId);
      this.fetchFoodDetails(foodIds);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['meal'] && this.meal?.foods?.length > 0) {
      const foodIds = this.meal.foods.map((food: any) => food.foodId);
      this.fetchFoodDetails(foodIds);
    }
  }

  fetchFoodDetails(foodIds: number[]): void {
    this.loading = true;
    this.error = null;

    this.mealService.getFoodDetailsByIds(foodIds).subscribe({
      next: (response: any) => {
        if (response.success) {
          this.foodDetails = response.data;
          // Map detailed nutritional information to each food
          this.meal.foods.forEach((food: any) => {
            const detailedInfo = this.foodDetails.find((detail: any) => detail.foodId === food.foodId);
            food.nutritionalInfo = detailedInfo || {};
          });
          this.toastrService.success('Food details loaded successfully.', 'Success');
        } else {
          this.error = response.message || 'Failed to fetch food details.';
          this.toastrService.error(this.error!, 'Error');
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'An error occurred while fetching food details.';
        this.toastrService.error(this.error, 'Error');
        this.loading = false;
      },
    });
  }

  closeDetails(): void {
    this.close.emit(); // Notify the parent to close the details view
  }
}
