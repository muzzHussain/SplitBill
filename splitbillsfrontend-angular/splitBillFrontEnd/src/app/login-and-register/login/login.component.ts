import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/Services/userService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  alert = false;
  loginForm!: FormGroup;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  userLogin: boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  get getEmail() {
    return this.loginForm.get('email');
  }

  get getPassword() {
    return this.loginForm.get('password');
  }

  async onUserLogin() {
    try {
      if (this.loginForm.valid) {
        this.userLogin = true;
        const email = this.loginForm.value.email;
        const password = this.loginForm.value.password;
        await this.userService.login(email, password);

        this.alertClass = 'alert alert-success';
        this.alert = true;
        this.successMessage = 'Login Successful';

        setTimeout(() => {
          this.route.navigateByUrl('/dashboard');
        }, 600);
      } else {
        this.validateAllFields(this.loginForm);
      }
    } catch (err) {
      this.loginForm = this.fb.group({
        email: '',
        passoword: '',
      });
    }
  }

  closeAlert() {
    this.alert = false;
  }

  private validateAllFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFields(control);
      }
    });
  }
}
