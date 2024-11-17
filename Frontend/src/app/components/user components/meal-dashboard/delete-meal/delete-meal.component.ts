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
  @Output() close = new EventEmitter<void>();
  @Output() success = new EventEmitter<void>();

  onDelete(): void {
    // Simulate API call to delete student
    console.log('Student deleted:', this.student);
    this.success.emit(); // Notify parent component
    this.close.emit(); // Close the delete form
  }
}
