import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { AddMealComponent } from '../add-meal/add-meal.component';
import { UpdateMealComponent } from '../update-meal/update-meal.component';
import { DeleteMealComponent } from '../delete-meal/delete-meal.component';
import { ToastrService } from 'ngx-toastr';
import { MealService } from '../../../../services/meal.service';// Assuming you have this service

@Component({
  selector: 'app-list-meal',
  standalone: true,
  imports: [
    CommonModule,
    AddMealComponent,
    UpdateMealComponent,
    DeleteMealComponent,
  ],
  templateUrl: './list-meal.component.html',
  styleUrl: './list-meal.component.css',
})
export class ListMealComponent implements OnInit {
  mealList: any[] = [];
  editing = false;
  deleting = false;
  selectedMeal: any = null;
  loading: boolean = false; // To show loading spinner
  retryTimeout: any = null; // To track retry timeout

  constructor(
    private mealService: MealService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadMeals();
  }

  loadMeals(): void {
    const username = localStorage.getItem('loggedUser');
    if (username) {
      this.loading = true; // Show loading spinner
      this.mealService.getMealsByUser(username).subscribe({
        next: (response) => {
          this.loading = false; // Hide loading spinner after response
          if (response.success) {
            console.log(response);
            // Sort meals by createdAt in reverse chronological order
            this.mealList = response.data.sort(
              (a: any, b: any) =>
                new Date(b.consumptionDate).getTime() -
                new Date(a.consumptionDate).getTime()
            );
            console.log(response);
            // Fetch the foods for each meal
            this.getFoodsForMeals();
            this.toastr.success('Meals retrieved successfully');
          } else {
            this.toastr.error(response.message || 'Failed to load meals');
          }
        },
        error: () => {
          this.loading = false; // Hide loading spinner
          this.toastr.error('Failed to load meals');
          this.retryAfterDelay(); // Prompt to retry after 1 minute
        }
      });
    }
  }

  retryAfterDelay() {
    // Show retry prompt after 1 minute
    this.retryTimeout = setTimeout(() => {
      this.toastr.info('Please retry to load meals', 'Retry', {
        closeButton: true,
        timeOut: 0,
        extendedTimeOut: 0,
        progressBar: true,
      }).onTap.subscribe(() => {
        this.loadMeals(); // Retry the API call
      });
    }, 60000); // 1 minute delay
  }
  // Method to fetch foods for the corresponding meal IDs
  getFoodsForMeals(): void {
    const mealIds = this.mealList.map((meal) => meal.mealId); // Extract meal IDs
    if (mealIds.length > 0) {
      this.mealService.getFoodsByMealIds(mealIds).subscribe({
        next: (response) => {
          if (response.success) {
            // Match the foods with meals based on mealId
            this.mealList.forEach((meal) => {
              meal.foods = response.data.filter(
                (food: any) => food.mealId === meal.mealId
              );
            });
            this.toastr.success('Foods retrieved successfully');
          } else {
            this.toastr.error(response.message || 'Failed to load foods');
          }
        },
        error: () => this.toastr.error('Failed to load foods'),
      });
    }
  }

  onEdit(meal: any): void {
    this.selectedMeal = meal;
    this.editing = true;
    this.deleting = false;
  }

  onDelete(meal: any): void {
    this.selectedMeal = meal;
    this.deleting = true;
    this.editing = false;
  }

  cancelEdit(): void {
    this.editing = false;
    this.selectedMeal = null;
  }

  cancelDelete(): void {
    this.deleting = false;
    this.selectedMeal = null;
  }

  refreshList(): void {
    this.loadMeals(); // Refresh the meal list
  }
}
