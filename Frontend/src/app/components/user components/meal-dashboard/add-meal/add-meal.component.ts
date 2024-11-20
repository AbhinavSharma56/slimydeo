import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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
export class AddMealComponent implements OnInit {
  @Output() success = new EventEmitter<void>();
  maxDate: string = "";

  meal = {
    mealId: 0,
    username: '',
    mealType: '',
    consumptionDate: this.getCurrentDateTime(),
  };
  foods: any[] = [
    {
      foodId: 0,
      foodName: '',
      quantity: 0,
      unit: '',
      mealId: 0,
    },
  ];
  foodItemsCount = 1;

  mealTypes = [
    { value: 'BREAKFAST', display: 'Breakfast' },
    { value: 'SNACK (MORNING)', display: 'Snack (Morning)' },
    { value: 'LUNCH', display: 'Lunch' },
    { value: 'SNACK (EVENING)', display: 'Snack (Evening)' },
    { value: 'DINNER', display: 'Dinner' },
  ];

  units: string[] = [
    'nos', 'no', 'cup', 'bowl (2 cups)', 'g', 'wt. oz', 'tsp', 
    'tbsp', 'cup', 'NLEA serving', 'oz', 'fl oz', 'glass', 
    'ml', 'l', 'g', 'mg'
  ];  

  constructor(
    private mealService: MealService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.maxDate = this.getCurrentDateTime();
  } 
  
  username = localStorage.getItem('loggedUser');

  // Helper method to get current date and time in 'YYYY-MM-DDTHH:mm' format
  getCurrentDateTime(): string {
    const now = new Date();
    return now.toISOString().slice(0, 16); // Format for datetime-local input
  }

  updateFoodItemsCount(): void {
    this.foods = Array.from({ length: this.foodItemsCount }, () => ({
      foodId: 0,
      foodName: '',
      quantity: 0,
      unit: '',
      mealId: 0,
    }));
  }

  addMeal(): void {
    if (this.username) {
      this.meal.username = this.username; // Assign logged user's username to the meal
      this.mealService.addMeal(this.meal).subscribe({
        next: (response) => {
          if (response.success) {
            const mealId = response.data.mealId;
            this.foods.forEach((food) => (food.mealId = mealId)); // Update mealId for each food item
            this.addFoods();
          }
        },
        error: () => this.toastr.error('Failed to add meal.'),
      });
    }
  }

  addFoods(): void {
    this.mealService.addFoodsBulk(this.foods).subscribe({
      next: (response) => {
        if (response.success) {
          this.toastr.success('Meal and foods added successfully.');
        } else {
          // Show error message from response if success is false
          this.toastr.error(response.message || 'Failed to add food details.');
        }
        this.success.emit();
        this.resetForm();
      },
      error: () => {
        // Show a generic error message in case of a network or other unexpected error
        this.toastr.error('Failed to add food details.');
      },
    });
  }
  

  resetForm(): void {
    this.meal = { 
      mealId: 0, 
      username: '', 
      mealType: '', 
      consumptionDate: this.getCurrentDateTime() };
    this.foodItemsCount = 1;
    this.updateFoodItemsCount();    
  }
}
