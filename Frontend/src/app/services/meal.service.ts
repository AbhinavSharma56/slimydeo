import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MealService {
  private apiUrl = 'https://localhost:7104/api'; // Base API

  constructor(private http: HttpClient) {}

  getMealsByUser(username: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Meal/user/${username}`);
  }

  getFoodsByMealIds(mealIds: number[]): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}/Food/foods-by-meal-ids`,
      mealIds
    );
  }

  getFoodsByMealId(mealId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Food/meal/${mealId}`);
  }

  addMeal(meal: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Meal`, meal);
  }

  addFoodsBulk(foods: any[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/Food/bulk-add`, foods);
  }

  // Delete meal by ID
  deleteMeal(mealId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Meal/${mealId}`);
  }

  // Update meal details by ID
  updateMeal(meal: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/Meal/${meal.mealId}`, meal);
  }

  updateFoodBulk(foods: any[]): Observable<any> {
    return this.http.put(`${this.apiUrl}/Food/bulk-update`, foods);
  }

  getFoodDetailsByIds(foodIds: number[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/FoodDetails/food-details-by-food-ids`, foodIds);
  }
}
