import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-exercise-dashboard',
  standalone: true,
  imports: [FormsModule, CommonModule],
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
    exerciseDate: this.getCurrentDateTime()
  };

  isEditing = false;
  isSubmitting = false;
  exerciseTypes: any[] = [];
  submittedLogs: any[] = [];
  maxDate: string = "";

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.maxDate = this.getCurrentDateTime();
    this.fetchPhysicalActivityLogs();
    this.fetchExerciseTypes();
  }

  user = localStorage.getItem('loggedUser');

  getCurrentDateTime(): string {
    const now = new Date();
    return now.toISOString().slice(0, 16); // Format for datetime-local input
  }
  // Fetch physical activity logs
  fetchPhysicalActivityLogs(): void {
    this.http
      .get<any[]>(`https://localhost:7210/api/ExerciseLog/user/${this.user}`)
      .subscribe({
        next: (data) => {
          this.submittedLogs = data;
        },
        error: (err) => {
          console.error('Error fetching logs:', err);
          this.toastr.error('Failed to load exercise logs.');
        },
      });
  }

  // Fetch exercise types
  fetchExerciseTypes(): void {
    this.http.get<any[]>('https://localhost:7210/api/ExerciseType').subscribe({
      next: (data) => {
        this.exerciseTypes = data;
      },
      error: (err) => {
        console.error('Error fetching exercise types:', err);
        this.toastr.error('Failed to load exercise types.');
      },
    });
  }

  // Get exercise name by ID
  getExerciseName(exerciseTypeId: number): string {
    const exercise = this.exerciseTypes.find(
      (type) => type.exerciseTypeId === exerciseTypeId
    );
    return exercise ? exercise.exerciseName : 'Unknown';
  }

  // Submit a new or edited log
  onSubmit(): void {
    this.isSubmitting = true;

    if (localStorage.getItem('loggedUser') !== null) {
      this.physicalActivityObj.username = localStorage.getItem('loggedUser')!;
    }

    const apiUrl = 'https://localhost:7210/api/ExerciseLog';
    const request = this.physicalActivityObj.logId === 0
      ? this.http.post(apiUrl, this.physicalActivityObj)
      : this.http.put(`${apiUrl}/${this.physicalActivityObj.logId}`, this.physicalActivityObj);

    request.subscribe({
      next: () => {
        this.toastr.success(
          `Log ${
            this.physicalActivityObj.logId === 0 ? 'created' : 'updated'
          } successfully!`
        );
        this.fetchPhysicalActivityLogs(); // Refresh logs
        this.resetForm();
      },
      error: (err) => {
        console.error('Error submitting log:', err);
        this.toastr.error('Failed to submit the log.');
      },
      complete: () => {
        this.isSubmitting = false;
      },
    });
  }

  // Edit a log
  editLog(log: any): void {
    this.physicalActivityObj = { ...log };
    this.isEditing = true;
  }

  // Delete a log by ID
  deleteLog(logId: number): void {
    if (!confirm('Are you sure you want to delete this log?')) return;

    this.http.delete(`https://localhost:7210/api/ExerciseLog/${logId}`).subscribe({
      next: (response: any) => {
        if (response.success) {
          this.toastr.success('Log deleted successfully!');
        } else {
          this.toastr.error('Failed to delete the log.');
        }
        this.fetchPhysicalActivityLogs(); // Refresh logs regardless of success or failure
      },
      error: (err) => {
        console.error('Error deleting log:', err);
        this.toastr.error('Error deleting log.');
      },
    });
  }

  // Reset the form after submission
  resetForm(): void {
    this.physicalActivityObj = {
      logId: 0,
      username: '',
      exerciseTypeId: '',
      duration: '',
      caloriesBurned: 0,
      exerciseDate: this.getCurrentDateTime()
    };
    this.isEditing = false;
  }
}
