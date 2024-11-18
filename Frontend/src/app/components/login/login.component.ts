import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ToastContainerDirective, ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { RecaptchaModule } from 'ng-recaptcha';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RecaptchaModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  captchaResolved = false; // Flag to enable the login button
  captchaResponse: string | null = null; // Store reCAPTCHA response

  constructor(
    private authService: AuthService,
    private toastrService: ToastrService
  ) {}

  router = inject(Router);

  @ViewChild(ToastContainerDirective, { static: true })
  toastContainer!: ToastContainerDirective;

  ngOnInit() {
    this.toastrService.overlayContainer = this.toastContainer;
  }

  loginObj: any = {
    userName: '',
    password: '',
  };

  resolved(captchaResponse: string) {
    this.captchaResponse = captchaResponse;
    this.captchaResolved = captchaResponse ? true : false; // Enable login button if resolved
  }

  onCaptchaResolved(captchaResponse: string): void {
    console.log(`Captcha resolved with response: ${captchaResponse}`);
  }

  onSubmit(form: NgForm) {
    if (this.captchaResolved) {
      this.authService.loginUser(this.loginObj).subscribe(
        (res: any) => {
          if (res.success) {
            // Store the user data in local storage
            localStorage.setItem('loggedUser', res.username);
            localStorage.setItem('loggedUserRole', res.role);
            localStorage.setItem('loggedUserToken', res.token);
            this.toastrService.success('Logged in successfully!!', 'Success');
            //Redirect to the dasboard after successful login
            if (res.role === 'ADMIN') {
              this.router.navigateByUrl('/admin');
            } else if (res.role === 'USER') {
              this.router.navigateByUrl('/user');
            }
          } else {
            this.toastrService.error(
              'There was an error while logging in. Please try again !!',
              'Error'
            );
            form.reset();
          }
        },
        (error: HttpErrorResponse) => {
          if (error.error && error.error.text) {
            // Display the error text in Toastr
            this.toastrService.error(error.error.text, 'Error');
          } else {
            // Handle generic error cases
            this.toastrService.error(
              'An error occurred: ' + error.message,
              'Error'
            );
          }
        }
      );
    }
  }
}
