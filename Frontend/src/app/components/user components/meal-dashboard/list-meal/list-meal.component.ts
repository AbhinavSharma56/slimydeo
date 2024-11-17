import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-list-meal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './list-meal.component.html',
  styleUrl: './list-meal.component.css'
})
export class ListMealComponent {
  studentList: any[] = [];
  selectedStudent: any;
  editing: boolean = false;
  deleting: boolean = false;
  successMessage: string = '';
  errorMessage: string = '';

  // studentService = inject(StudentService);

  ngOnInit(): void {
    this.loadStudents();
  }

  loadStudents() {
    // this.studentService.getStudents().subscribe(
    //   (res: any) => {
    //     this.studentList = res;
    //   },
    //   (error) => {
    //     this.errorMessage = 'Error loading student data.';
    //   }
    // );
  }

  onEdit(student: any) {
    this.selectedStudent = student;
    this.editing = true;  // Show the update component
    this.deleting = false; // Hide the delete component
  }

  onDelete(student: any) {
    this.selectedStudent = student;
    this.deleting = true;  // Show the delete component
    this.editing = false;   // Hide the update component
  }

  cancelEdit() {
    this.editing = false;
    this.selectedStudent = null;
  }

  cancelDelete() {
    this.deleting = false;
    this.selectedStudent = null;
  }

  handleSuccess(message: string) {
    this.successMessage = message;
    this.loadStudents();  // Refresh the list after successful operation
    this.autoCloseAlert('success');  
  }

  handleError(errorMessage: string) {
    alert(errorMessage);  // Display the error message
    this.autoCloseAlert('error');
  }

  autoCloseAlert(type: 'success' | 'error') {
    setTimeout(() => {
      if (type === 'success') {
        this.successMessage = ''; // Clear success message after 5 seconds
      } else {
        this.errorMessage = ''; // Clear error message after 5 seconds
      }
    }, 5000);
  }
}
