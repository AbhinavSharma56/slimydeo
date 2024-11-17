import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-meal',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-meal.component.html',
  styleUrls: ['./add-meal.component.css'], // Corrected property name
})
export class AddMealComponent {
  student = { studentName: '', studentGrade: '', studentRollNo: '' };

  @Output() success = new EventEmitter<void>(); // Emits success event to parent

  onAdd(): void {
    // Simulate API call to add student
    if (this.isFormValid()) {
      console.log('Student added:', this.student);
      this.resetForm(); // Clear form fields after adding
      this.success.emit(); // Notify parent component of success
    } else {
      console.error('Form validation failed. Please fill all required fields.');
    }
  }

  onCancel(): void {
    // Logic to handle cancellation, if needed
    console.log('Add operation cancelled');
    this.resetForm();
  }

  private isFormValid(): boolean {
    // Simple form validation logic
    return (
      this.student.studentName.trim() !== '' &&
      this.student.studentGrade.trim() !== '' &&
      this.student.studentRollNo.trim() !== ''
    );
  }

  private resetForm(): void {
    // Reset the form fields
    this.student = { studentName: '', studentGrade: '', studentRollNo: '' };
  }
}