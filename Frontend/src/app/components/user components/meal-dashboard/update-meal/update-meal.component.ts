import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update-meal',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './update-meal.component.html',
  styleUrl: './update-meal.component.css'
})
export class UpdateMealComponent{
  @Input() student: any;
  @Output() close = new EventEmitter<void>();
  @Output() success = new EventEmitter<void>();

  onSubmit(): void {
    // Simulate API call to update student
    console.log('Student updated:', this.student);
    this.success.emit(); // Notify parent component
    this.close.emit(); // Close the update form
  }
}
