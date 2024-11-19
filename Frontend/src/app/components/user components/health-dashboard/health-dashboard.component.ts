import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-health-dashboard',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './health-dashboard.component.html',
  styleUrls: ['./health-dashboard.component.css'],
})
export class HealthDashboardComponent {}