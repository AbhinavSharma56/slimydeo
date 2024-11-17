import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-meal',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-meal.component.html',
  styleUrl: './add-meal.component.css'
})
export class AddMealComponent {
  studentObj: any = {
    "studentId": 0,
    "studentName": "",
    "studentGrade": "",
    "studentRollNo": "",
    "isActive": true,
    "createdDate": this.getCurrentLocalDateTime(), // Set to local time
    "modifiedDate": this.getCurrentLocalDateTime() // Set to local time
  }

  getCurrentLocalDateTime(): string {
    const now = new Date();
    const offsetMs = now.getTimezoneOffset() * 60 * 1000;
    const localDateTime = new Date(now.getTime() - offsetMs).toISOString().slice(0, 16);
    return localDateTime;
  }

  http = inject(HttpClient);
  onSubmit(form: NgForm) {
    debugger;
    this.http.post(
      "https://localhost:7088/api/TblStudents",
      this.studentObj
    ).subscribe(
      (res: any) => {
        if (res.studentId >= 0) {
          alert("Student Record Created!");

          // Reset the form to its initial state
          form.resetForm({
            studentId: 0,
            studentName: '',
            studentGrade: '',
            studentRollNo: '',
            isActive: true,
            createdDate: this.getCurrentLocalDateTime(),
            modifiedDate: this.getCurrentLocalDateTime()
          });
        } else {
          alert("There was an error while creating the student record.");
        }
      }
    );
  }
}
