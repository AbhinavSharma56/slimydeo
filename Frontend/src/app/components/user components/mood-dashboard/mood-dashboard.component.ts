import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { MoodService } from '../../../services/mood.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-mood-dashboard',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterOutlet],
  templateUrl: './mood-dashboard.component.html',
  styleUrl: './mood-dashboard.component.css',
})
export class MoodDashboardComponent {
  submittedLogs: any[] = []; // Array to store the logs fetched from the backend
  newLog: any = {
    username: '',
    moodId: 0,
    intensity: 1,
    notes: '',
    logDate: this.getCurrentDateTime(),
  };
  moods: any[] = [];

  getCurrentDateTime(): string {
    const now = new Date();
    return now.toISOString().slice(0, 16); // Format for datetime-local input
  }
  username = localStorage.getItem('loggedUser');

  constructor(
    private http: HttpClient,
    private cd: ChangeDetectorRef,
    private moodService: MoodService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadMoods();
    this.fetchMentalHealthLogs(); // Fetch existing logs on component initialization
  }

  // Fetch the list of existing mental health logs from the API
  fetchMentalHealthLogs(): void {
    this.http
      .get<any[]>(`https://localhost:7283/api/MentalHealthLog/user/${this.username}`)
      .subscribe({
        next: (data) => {
          this.submittedLogs = data;
          this.submittedLogs.forEach((log) => {
            const mood = this.moods.find((m) => m.moodId === log.moodId);
            if (mood) {
              log.moodName = mood.moodName;
              log.moodDescription = mood.description;
            }
          });
          this.cd.detectChanges(); // Ensure view updates after data changes
        },
        error: (error) => {
          this.toastr.error('Error fetching mental health logs', 'Error');
          console.error('Error fetching mental health logs:', error);
        },
      });
  }

  // Fetch the list of moods from the API
  loadMoods(): void {
    this.moodService.getMoods().subscribe({
      next: (data) => {
        this.moods = data;
      },
      error: (error) => {
        this.toastr.error('Error fetching moods', 'Error');
        console.error('Error fetching moods:', error);
      },
    });
  }

  // Add new log
  addLog(): void {
    this.newLog.username = this.username;
    if (this.newLog.username && this.newLog.moodId && this.newLog.intensity) {
      this.http.post('https://localhost:7283/api/MentalHealthLog', this.newLog).subscribe({
        next: (response: any) => {
          if(response.success) {
          this.toastr.success('Mental health log added successfully', 'Success');
          this.fetchMentalHealthLogs(); // Reload logs after adding the new one
          this.resetForm(); // Reset the form fields
          } else {
            this.toastr.error('Failed to add mental health log', 'Error');
          }
        },
        error: (error) => {
          this.toastr.error('Error adding mental health log', 'Error');
          console.error('Error adding mental health log:', error);
        },
      });
    } else {
      this.toastr.warning('Please fill all required fields', 'Warning');
    }
  }

  // Reset the form fields after submission
  resetForm(): void {
    this.newLog = {
      username: '',
      moodId: 0,
      intensity: 1,
      notes: '',
      logDate: this.getCurrentDateTime(),
    };
  }

  // Function to delete a log by its ID
  deleteLog(logId: number): void {
    const confirmDelete = confirm('Are you sure you want to delete this log?');
    if (confirmDelete) {
      this.http.delete(`https://localhost:7283/api/MentalHealthLog/${logId}`).subscribe({
        next: (response: any) => {
          if(response.success) {
          this.toastr.success('Log deleted successfully', 'Success');
          
          } else {
            this.toastr.error('Failed to delete log', 'Error');
          }
          this.loadMoods();
          this.fetchMentalHealthLogs(); // Reload logs after deletion
        },
        error: (error) => {
          this.loadMoods();
          this.fetchMentalHealthLogs(); // Reload logs after deletion
          this.toastr.error('Error deleting log', 'Error');
          console.error('Error deleting log:', error);
        },
      });
    }
  }
}
