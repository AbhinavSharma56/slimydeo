import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-index-navbar',
  standalone: true,
  imports: [RouterLink, RouterOutlet],
  templateUrl: './index-navbar.component.html',
  styleUrl: './index-navbar.component.css'
})
export class IndexNavbarComponent {

}
