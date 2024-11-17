import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update-meal',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './update-meal.component.html',
  styleUrl: './update-meal.component.css'
})
export class UpdateMealComponent implements OnInit{
  @Input() student: any; // Input property to receive student data
  @Output() close = new EventEmitter<void>(); // Emit an event to close the update form
  @Output() success = new EventEmitter<string>(); // Emit success message
  @Output() error = new EventEmitter<string>(); // Emit error message

  //constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    if (!this.student) {
      this.resetForm();
    }
  }

  resetForm() {
    this.student = {
      studentId: 0,
      studentName: '',
      studentGrade: '',
      studentRollNo: '',
      isActive: true,
      createdDate: this.getCurrentLocalDateTime(),
      modifiedDate: this.getCurrentLocalDateTime(),
    };
  }

  getCurrentLocalDateTime(): string {
    const now = new Date();
    const offsetMs = now.getTimezoneOffset() * 60 * 1000;
    const localDateTime = new Date(now.getTime() - offsetMs)
      .toISOString()
      .slice(0, 16);
    return localDateTime;
  }
  onUpdate() {
    // Set modifiedDate to current time before updating
    this.student.modifiedDate = this.getCurrentLocalDateTime();

    // this.studentService.updateStudent(this.student).subscribe(
    //   (res: any) => {
    //     this.success.emit('Student updated successfully!');
    //     this.close.emit(); // Emit close event
    //   },
    //   (error) => {
    //     this.error.emit('Some problem occurred during update');
    //   }
    // );
  }

  cancel() {
    this.close.emit(); // Emit close event
  }
}
