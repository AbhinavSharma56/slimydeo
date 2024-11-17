import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-exercise-dashboard',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterOutlet],
  templateUrl: './exercise-dashboard.component.html',
  styleUrl: './exercise-dashboard.component.css',
})
export class ExerciseDashboardComponent {
  physicalActivityObj = {
    logId: 0,
    username: '',
    exerciseTypeId: '',
    duration: '',
    caloriesBurned: 0,
    exerciseDate: '',
  };

  isEditing = false;
  isSubmitting = false;
  exerciseTypes: any[] = [];
  submittedLogs: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchPhysicalActivityLogs();
    this.fetchExerciseTypes();
  }

  fetchPhysicalActivityLogs(): void {
    this.http.get<any[]>('https://localhost:7210/api/ExerciseLog').subscribe(
      (data) => (this.submittedLogs = data),
      (err) => console.error('Error fetching logs', err)
    );
  }

  fetchExerciseTypes(): void {
    this.http.get<any[]>('https://localhost:7210/api/ExerciseType').subscribe(
      (data) => (this.exerciseTypes = data),
      (err) => console.error('Error fetching exercise types', err)
    );
  }

  getExerciseName(exerciseTypeId: number): string {
    const exercise = this.exerciseTypes.find(
      (type) => type.exerciseTypeId === exerciseTypeId
    );
    return exercise ? exercise.exerciseName : 'Unknown';
  }

  onSubmit(): void {
    this.isSubmitting = true;
    if (localStorage.getItem('loggedUser') !== null) {
      this.physicalActivityObj.username = localStorage.getItem('loggedUser')!;
    }
    const apiUrl =
      this.physicalActivityObj.logId === 0
        ? 'https://localhost:7210/api/ExerciseLog'
        : `https://localhost:7210/api/ExerciseLog/${this.physicalActivityObj.logId}`;

    const request =
      this.physicalActivityObj.logId === 0
        ? this.http.post(apiUrl, this.physicalActivityObj)
        : this.http.put(apiUrl, this.physicalActivityObj);

    request.subscribe({
      next: () => {
        alert(
          `Log ${
            this.physicalActivityObj.logId === 0 ? 'created' : 'updated'
          } successfully!`
        );
        this.fetchPhysicalActivityLogs(); // Refresh logs
        this.resetForm();
      },
      error: (err) => alert(`Error: ${err.message}`),
      complete: () => (this.isSubmitting = false),
    });
  }

  editLog(log: any): void {
    this.physicalActivityObj = { ...log };
    this.isEditing = true;
  }

  deleteLog(logId: number): void {
    if (!confirm('Are you sure you want to delete this log?')) return;

    this.http
      .delete(`https://localhost:7210/api/ExerciseLog/${logId}`)
      .subscribe({
        next: () => {
          alert('Log deleted successfully!');
          this.fetchPhysicalActivityLogs(); // Refresh logs
        },
        error: (err) => alert(`Error: ${err.message}`),
      });
  }

  resetForm(): void {
    this.physicalActivityObj = {
      logId: 0,
      username: '',
      exerciseTypeId: '',
      duration: '',
      caloriesBurned: 0,
      exerciseDate: '',
    };
    this.isEditing = false;
  }
}
