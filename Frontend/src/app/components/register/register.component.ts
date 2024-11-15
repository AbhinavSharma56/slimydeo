import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ToastContainerDirective, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  constructor(private authService: AuthService, private toastrService: ToastrService) {}
  @ViewChild(ToastContainerDirective, { static: true })
  toastContainer!: ToastContainerDirective;

  ngOnInit() {
    this.toastrService.overlayContainer = this.toastContainer;
  }
  
  registerObj: any = {
    userName: '',
    password: '',
    email: '',
    role: '',
  }
  registerDetailsObj: any = {
      userProfileId: 0,
      username: '',
      fullName: '',
      dob: '',
      email: '',
      mobileNumber: 0,
      gender: '',
      address: '',
      profilePicture: '',
      createdAt: this.getCurrentLocalDateTime(), // Set to local time,
      weight: 0,
      height: 0
  };

  getCurrentLocalDateTime(): string {
    const now = new Date();
    const offsetMs = now.getTimezoneOffset() * 60 * 1000;
    const localDateTime = new Date(now.getTime() - offsetMs)
      .toISOString()
      .slice(0, 16);
    return localDateTime;
  }

  onEmailChange(email: string) {
    this.registerObj.userName = email; // Set email as username
    this.registerDetailsObj.email = email; // Update registerDetailsObj's email
    this.registerDetailsObj.username = email; // Update registerDetailsObj's username
  }

  onSubmit(form: NgForm) {
    this.authService.registerUser(this.registerObj).subscribe(
      (res: any) => {
        if (res) {
          this.toastrService.success('Registered successfully !', 'Success');
          // Reset the form to its initial state
          form.resetForm({
            userName: '',
            password: '',
            email: '',
            role: '',
          });
        } else {
          this.toastrService.error('There was an error while registering the user.', 'Error');
        }
      },
      (error) => {
        this.toastrService.error('An error occurred: ' + error.message, 'Error');
      }
    );
  }
}