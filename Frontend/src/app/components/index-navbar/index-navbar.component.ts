import { Component } from '@angular/core';
import { HomeComponent } from '../home/home.component';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-index-navbar',
  standalone: true,
  imports: [ HomeComponent, LoginComponent, RegisterComponent, RouterLink, RouterOutlet],
  templateUrl: './index-navbar.component.html',
  styleUrl: './index-navbar.component.css'
})
export class IndexNavbarComponent {

}
