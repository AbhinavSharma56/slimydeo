import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-mood-type',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './mood-type.component.html',
  styleUrl: './mood-type.component.css'
})
export class MoodTypeComponent {
  // MoodType object to hold form data
  moodTypeObj = {
    moodId: 0,
    moodName: '',
    description: '',
    createdBy: 0,
    createdAt: ''
  };

  // Array to hold fetched mood types
  submittedMoodTypes: any[] = [];
  isSubmitting = false;  // Track the submission status
  isEditing = false; // Track if the form is in edit mode

  constructor(private http: HttpClient, private cd: ChangeDetectorRef) { }  // Constructor Injection

  // Function to handle the form submission
  onSubmit(): void {
    this.isSubmitting = true;

    if (this.moodTypeObj.moodId === 0) {
      // If moodId is 0, it's a new mood type, so POST request is made
      this.http.post("https://localhost:7283/api/Mood", this.moodTypeObj)
        .subscribe(
          (res: any) => {
            if (res.moodId >= 0) {
              alert("Mood Type Created!");
              this.submittedMoodTypes.push(res);  // Add the new mood type to the local list immediately
            } else {
              alert("Some Problem in Mood Type Creation");
            }
            this.isSubmitting = false;  // Reset submitting state
            this.resetForm();  // Reset form after submission
            this.cd.detectChanges(); // Trigger change detection
          }
        );
    } else {
      // If moodId is not 0, it's an existing mood type, so PUT request is made to update
      this.http.put(`https://localhost:7283/api/Mood/${this.moodTypeObj.moodId}`, this.moodTypeObj)
        .subscribe(
          (res: any) => {
            if (res.moodId >= 0) {
              alert("Mood Type Updated!");
              // Update the mood type in the local list immediately
              const index = this.submittedMoodTypes.findIndex(mood => mood.moodId === this.moodTypeObj.moodId);
              if (index !== -1) {
                // Directly mutate the object at that index with the updated data
                this.submittedMoodTypes[index] = { ...this.moodTypeObj };  // Spread the updated values
              }
            } else {
              alert("Some Problem in Mood Type Update");
            }
            this.isSubmitting = false;  // Reset submitting state
            this.resetForm();  // Reset form after update
            this.cd.detectChanges(); // Trigger change detection
          }
        );
    }
  }

  // Function to fetch mood types from the backend
  fetchMoodTypes(): void {
    this.http.get<any[]>('https://localhost:7283/api/Mood')
      .subscribe(
        (data) => {
          this.submittedMoodTypes = data;
          this.cd.detectChanges(); // Trigger change detection
        }
      );
  }

  // Function to edit an existing mood type
  editMood(mood: any): void {
    this.moodTypeObj = { ...mood }; // Pre-fill the form with mood data
    this.isEditing = true;  // Set the form to editing mode
  }

  // Function to reset the form
  resetForm(): void {
    this.moodTypeObj = {
      moodId: 0,
      moodName: '',
      description: '',
      createdBy: 0,
      createdAt: ''
    };
    this.isEditing = false; // Reset editing flag
  }

  // On component initialization, fetch the mood types
  ngOnInit(): void {
    this.fetchMoodTypes();
  }
}
