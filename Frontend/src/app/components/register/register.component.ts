import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ToastContainerDirective, ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { saveAs } from 'file-saver';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private toastrService: ToastrService,
    private userService: UserService
  ) {}

  @ViewChild(ToastContainerDirective, { static: true })
  toastContainer!: ToastContainerDirective;

  confirmPassword: string = '';
  passwordsMatch: boolean = true;
  mobileNumberError: string = '';

  ngOnInit() {
    this.toastrService.overlayContainer = this.toastContainer;
  }

  registerObj: any = {
    userName: '',
    password: '',
    email: '',
    role: '',
  };
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
    height: 0,
  };

  getCurrentLocalDateTime(): string {
    const now = new Date();
    const offsetMs = now.getTimezoneOffset() * 60 * 1000;
    const localDateTime = new Date(now.getTime() - offsetMs)
      .toISOString()
      .slice(0, 16);
    return localDateTime;
  }

  // Email validation method
  onEmailChange(email: string) {
    if (email) {
      this.validateEmail(email);
    }
    this.registerObj.userName = email; // Set email as username
    this.registerDetailsObj.email = email; // Update registerDetailsObj's email
    this.registerDetailsObj.username = email; // Update registerDetailsObj's username
  }

  validateEmail(email: string): void {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailPattern.test(email)) {
      console.log('Invalid email format');
    }
  }

  // Check if password and confirm password match
  checkPasswordsMatch(): void {
    this.passwordsMatch = this.registerObj.password === this.confirmPassword;
  }

  passwordError: string | null = null;

  validatePassword(): void {
    const password = this.registerObj.password;

    if (!password) {
      this.passwordError = 'Password is required.';
    } else if (!/[A-Z]/.test(password)) {
      this.passwordError =
        'Password must contain at least one uppercase letter.';
    } else if (!/[a-z]/.test(password)) {
      this.passwordError =
        'Password must contain at least one lowercase letter.';
    } else if (!/\d/.test(password)) {
      this.passwordError = 'Password must contain at least one number.';
    } else if (!/[@$!%*?&]/.test(password)) {
      this.passwordError =
        'Password must contain at least one special character.';
    } else {
      this.passwordError = null; // No errors
    }
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        if (e.target?.result) {
          const data = new Blob([e.target.result], { type: file.type });
          saveAs(data, file.name, { autoBom: true });
        } else {
          // Handle file reading error (e.g., display an error message)
        }
      };
    }
  }

  onSubmit(form: NgForm): void {
    this.checkPasswordsMatch();

    if (!this.passwordsMatch) {
      this.toastrService.error('Passwords do not match!', 'Error');
      return;
    }

    this.authService.registerUser(this.registerObj).subscribe(
      (res: any) => {
        if (res) {
          this.toastrService.success('Registered successfully !', 'Success');
          // Reset the form to its initial state
          form.resetForm();
        } else {
          this.toastrService.error(
            'There was an error while registering the user.',
            'Error'
          );
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
