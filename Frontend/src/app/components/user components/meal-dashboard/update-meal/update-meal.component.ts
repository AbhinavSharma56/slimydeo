import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MealService } from '../../../../services/meal.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-meal',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './update-meal.component.html',
  styleUrls: ['./update-meal.component.css'],
})
export class UpdateMealComponent implements OnChanges {
  @Input() meal: any; // Meal object passed from parent
  @Output() close = new EventEmitter<void>(); // Notify parent to close the modal
  @Output() success = new EventEmitter<void>(); // Notify parent on successful update
  @Output() reload = new EventEmitter<void>()

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
    if (this.meal?.mealId) {
      this.loadFoods(); // Load foods if the mealId exists
    }
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
          this.toastr.error(response.message || 'Failed to load food details.');
        }
      },
      error: () => {
        this.toastr.error('Failed to load food details.');
      },
    });
  }

  // Update the meal information
  updateMeal(): void {
    const updatedMeal = {
      mealId: this.meal.mealId,
      username: localStorage.getItem('loggedUser'),
      mealType: this.meal.mealType,
      consumptionDate: this.meal.consumptionDate,
    };

    this.mealService.updateMeal(updatedMeal).subscribe({
      next: (response) => {
        if (response.success) {
          this.success.emit(); // Notify parent of success
          this.close.emit(); // Close the form
          this.updateFoods(); // After meal update, update the foods
        } else {
          this.toastr.error(response.message || 'Failed to update meal.');
        }
      },
      error: () => this.toastr.error('Failed to update meal.'),
    });
  }

  // Update food items in bulk after meal update
  updateFoods(): void {
    this.mealService.updateFoodBulk(this.foods).subscribe({
      next: (response) => {
        if (response.success) {
          this.toastr.success('Meal and foods updated successfully.');
          this.success.emit(); // Notify parent of success
          this.close.emit(); // Close the form
        } else {
          this.toastr.error(
            response.message || 'Failed to update food details.'
          );
        }
      },
      error: () => {
        this.toastr.error('Failed to update food details.');
      },
    });
  }
  

  // Cancel and close the update form
  cancel(): void {
    this.close.emit(); // Notify parent to close the form
    this.reload.emit();
  }
}
