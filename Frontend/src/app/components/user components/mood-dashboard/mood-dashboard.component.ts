import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { MoodService } from '../../../services/mood.service';

@Component({
  selector: 'app-mood-dashboard',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterOutlet],
  templateUrl: './mood-dashboard.component.html',
  styleUrl: './mood-dashboard.component.css'
})
export class MoodDashboardComponent {
  submittedLogs: any[] = [];  // Array to store the logs fetched from the backend
  newLog: any = {             // Object to store the data from the form
    username: '',
    moodId: 0,
    intensity: 1,
    notes: '',
    logDate: ''
  };
  moods: any[] = [];

  constructor(private http: HttpClient, private cd: ChangeDetectorRef, private moodService: MoodService) {}

  ngOnInit(): void {
    this.fetchMentalHealthLogs();  // Fetch existing logs on component initialization
    this.loadMoods();
  }

  // Fetch the list of existing mental health logs from the API
  fetchMentalHealthLogs(): void {
    this.http.get<any[]>('https://localhost:7283/api/MentalHealthLog')
      .subscribe(
        (data) => {
          this.submittedLogs = data;  // Store fetched data
          this.cd.detectChanges();     // Ensure view updates after data changes
        },
        (error) => {
          console.error('Error fetching mental health logs:', error);
        }
      );
  }

  loadMoods(): void {
    this.moodService.getMoods().subscribe(
      (data) => {
        this.moods = data; // Assuming the API returns an array of moods
      },
      (error) => {
        console.error('Error fetching moods:', error);
      }
    );
  }

  // Add new log
  addLog(): void {
    this.newLog.username = localStorage.getItem('loggedUser');
    if (this.newLog.username && this.newLog.moodId && this.newLog.intensity) {
      this.http.post('https://localhost:7283/api/MentalHealthLog', this.newLog)
        .subscribe(
          () => {
            this.fetchMentalHealthLogs();  // Reload the logs after adding the new one
            this.resetForm();              // Reset the form fields
          },
          (error) => {
            console.error('Error adding mental health log:', error);
          }
        );
    } else {
      console.error("Please fill all required fields");
    }
  }

  // Reset the form fields after submission
  resetForm(): void {
    this.newLog = {
      username: '',
      moodName: '',
      intensity: 1,
      notes: '',
      logDate: new Date()
    };
  }

  // Function to delete a log by its ID
  deleteLog(logId: number): void {
    const confirmDelete = confirm('Are you sure you want to delete this log?');
    if (confirmDelete) {
      this.http.delete(`https://localhost:7283/api/MentalHealthLog/${logId}`)
        .subscribe(
          () => {
            this.fetchMentalHealthLogs();  // Reload the logs after deletion
          },
          (error) => {
            console.error('Error deleting log:', error);
          }
        );
    }
  }
}
