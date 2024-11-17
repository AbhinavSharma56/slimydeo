import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, inject } from '@angular/core';

@Component({
  selector: 'app-delete-meal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './delete-meal.component.html',
  styleUrl: './delete-meal.component.css'
})
export class DeleteMealComponent {
  @Input() student: any;
  @Output() close = new EventEmitter<void>(); // Emit close event
  @Output() success = new EventEmitter<string>(); // Emit success message
  @Output() error = new EventEmitter<string>(); // Emit error message

  //studentService = inject(StudentService);
  successMessage: string = '';
  errorMessage: string = '';

  onDelete() {
    // this.studentService.deleteStudent(this.student.studentId).subscribe(
    //   (res: any) => {
    //     this.success.emit('Student deleted successfully!');
    //     this.close.emit(); // Emit close event
    //   },
    //   (error) => {
    //     this.error.emit('Error occurred while deleting the student.');
    //   }
    // );
  }

  cancel() {
    this.close.emit(); // Emit close event
  }
}
