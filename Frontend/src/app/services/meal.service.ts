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
    return this.http.post<any>(`${this.apiUrl}/Food/foods-by-meal-ids`, mealIds);
  }

  addMeal(meal: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Meal`, meal);
  }

  addFoodDetailsBulk(mealId: number, foodDetails: any[]): Observable<any> {
    const body = foodDetails.map((food) => ({ ...food, mealId }));
    return this.http.post(`${this.apiUrl}/FoodDetails/add-food-details-bulk`, body);
  }
}