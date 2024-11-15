import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ToastContainerDirective, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private toastrService: ToastrService
  ) {}

  @ViewChild(ToastContainerDirective, { static: true })
  toastContainer!: ToastContainerDirective;

  ngOnInit() {
    this.toastrService.overlayContainer = this.toastContainer;
  }

  loginObj: any = {
    userName: '',
    password: '',
  };

  onSubmit(form: NgForm) {
    debugger;
    this.authService.loginUser(this.loginObj).subscribe(
      (res: any) => {
        if (res) {
          this.toastrService.success('Logged in successfully !!', 'Success');
        } else {
          alert('There was an error while loggin in. Please try again !');
        }
      },
      (error) => {
        this.toastrService.error(
          'An error occurred: ' + error.message,
          'Error'
        );
      }
    );
  }
}
