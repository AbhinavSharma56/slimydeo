import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-exercise-type',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './exercise-type.component.html',
  styleUrl: './exercise-type.component.css',
})
export class ExerciseTypeComponent {
  // Exercise object to hold form data
  exerciseObj = {
    exerciseTypeId: 0,
    exerciseName: '',
    description: '',
    createdBy: '',
    createdAt: '',
  };

  // Array to hold fetched exercises
  submittedExercises: any[] = [];
  isSubmitting = false; // Track the submission status
  isEditing = false; // Track if the form is in edit mode

  constructor(private http: HttpClient, private cd: ChangeDetectorRef) {}

  // Function to handle the form submission
  onSubmit(): void {
    this.isSubmitting = true;

    if (this.exerciseObj.exerciseTypeId === 0) {
      // If exerciseTypeId is 0, it's a new log, so POST request is made
      this.http
        .post('https://localhost:7210/api/ExerciseType', this.exerciseObj)
        .subscribe((res: any) => {
          if (res.exerciseTypeId >= 0) {
            alert('Exercise Record Created!');
            this.submittedExercises.push(res); // Add the new exercise to the local list immediately
          } else {
            alert('Problem in Exercise Creation');
          }
          this.isSubmitting = false; // Reset submitting state
          this.resetForm(); // Reset form after submission
          this.cd.detectChanges(); // Trigger change detection
        });
    } else {
      // If exerciseTypeId is not 0, it's an existing log, so PUT request is made to update
      this.http
        .put(
          `https://localhost:7210/api/ExerciseType/${this.exerciseObj.exerciseTypeId}`,
          this.exerciseObj
        )
        .subscribe((res: any) => {
          if (res.exerciseTypeId >= 0) {
            alert('Exercise Record Updated!');
            // Update the exercise in the local list immediately
            const index = this.submittedExercises.findIndex(
              (exercise) =>
                exercise.exerciseTypeId === this.exerciseObj.exerciseTypeId
            );
            if (index !== -1) {
              this.submittedExercises[index] = { ...this.exerciseObj }; // Spread the updated values
            }
          } else {
            alert('Problem in Exercise Update');
          }
          this.isSubmitting = false; // Reset submitting state
          this.resetForm(); // Reset form after update
          this.cd.detectChanges(); // Trigger change detection
        });
    }
  }

  // Function to fetch exercise logs from the backend
  fetchExerciseLogs(): void {
    this.http
      .get<any[]>('https://localhost:7210/api/ExerciseType')
      .subscribe((data) => {
        this.submittedExercises = data;
        this.cd.detectChanges(); // Trigger change detection
      });
  }

  // Function to edit an existing exercise log
  editExercise(exercise: any): void {
    this.exerciseObj = { ...exercise }; // Pre-fill the form with exercise data
    this.isEditing = true; // Set the form to editing mode
  }

  // Function to delete an existing exercise log
  deleteExercise(exerciseTypeId: number): void {
    if (confirm('Are you sure you want to delete this log?')) {
      this.http
        .delete(`https://localhost:7210/api/ExerciseType/${exerciseTypeId}`)
        .subscribe((res: any) => {
          if (res) {
            alert('Exercise Record Deleted!');
            // Remove the deleted exercise from the local list
            this.submittedExercises = this.submittedExercises.filter(
              (exercise) => exercise.exerciseTypeId !== exerciseTypeId
            );
            this.cd.detectChanges(); // Trigger change detection
          } else {
            alert('Problem deleting the exercise record.');
          }
        });
    }
  }

  // Function to reset the form
  resetForm(): void {
    this.exerciseObj = {
      exerciseTypeId: 0,
      exerciseName: '',
      description: '',
      createdBy: '',
      createdAt: '',
    };
    this.isEditing = false; // Reset editing flag
  }

  // On component initialization, fetch the exercises
  ngOnInit(): void {
    this.fetchExerciseLogs();
  }
}
