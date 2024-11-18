import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MealService } from '../../../../services/meal.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-meal',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './update-meal.component.html',
  styleUrl: './update-meal.component.css',
})
export class UpdateMealComponent implements OnChanges {
  @Input() meal: any; // Meal object passed from parent
  @Output() close = new EventEmitter<void>(); // Notify parent to close the modal
  @Output() success = new EventEmitter<void>(); // Notify parent on successful update

  foods: any[] = [];
  mealTypes = [
    { value: 'BREAKFAST', display: 'Breakfast' },
    { value: 'SNACK (MORNING)', display: 'Snack (Morning)' },
    { value: 'LUNCH', display: 'Lunch' },
    { value: 'SNACK (EVENING)', display: 'Snack (Evening)' },
    { value: 'DINNER', display: 'Dinner' },
  ];
  units: string[] = [
    'nos',
    'no',
    'cup',
    'bowl (2 cups)',
    'g',
    'wt. oz',
    'tsp',
    'tbsp',
    'cup',
    'NLEA serving',
    'oz',
    'fl oz',
    'glass',
    'ml',
    'l',
    'g',
    'mg',
  ];

  constructor(
    private mealService: MealService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    // Load existing foods associated with the meal
    this.loadFoods();
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['meal'] && this.meal?.mealId) {
      this.loadFoods(); // Refresh foods whenever a new meal is selected
    }
  }

  loadFoods(): void {
    this.mealService.getFoodsByMealId(this.meal.mealId).subscribe({
      next: (response) => {
        if (response.success) {
          this.foods = response.data; // Populate foods array with response data
          this.toastr.success('Food details loaded successfully.');
        } else {
          // Display the error message from the response if available
          this.toastr.error(response.message || 'Failed to load food details.');
        }
      },
      error: () => {
        // Generic error message in case of network or other unexpected errors
        this.toastr.error('Failed to load food details.');
      },
    });
  }  

  updateMeal(): void {
    // this.mealService.updateMeal(this.meal).subscribe({
    //   next: (response) => {
    //     if (response.success) {
    //       this.updateFoods();
    //     } else {
    //       this.toastr.error(response.message || 'Failed to update meal.');
    //     }
    //   },
    //   error: () => this.toastr.error('Failed to update meal.'),
    // });
  }

  updateFoods(): void {
    // this.mealService.updateFoodsBulk(this.foods).subscribe({
    //   next: (response) => {
    //     if (response.success) {
    //       this.toastr.success('Meal and foods updated successfully.');
    //       this.success.emit(); // Notify parent of success
    //       this.close.emit(); // Close the form
    //     } else {
    //       this.toastr.error(
    //         response.message || 'Failed to update food details.'
    //       );
    //     }
    //   },
    //   error: () => this.toastr.error('Failed to update food details.'),
    // });
  }

  cancel(): void {
    this.close.emit(); // Notify parent to close the form
  }
}
