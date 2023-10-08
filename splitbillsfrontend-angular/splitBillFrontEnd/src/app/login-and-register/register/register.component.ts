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
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  alert = false;
  registerForm!: FormGroup;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  userLogin: boolean = false;

  constructor(
    private _fb: FormBuilder,
    private _route: Router,
    private _userService: UserService
  ) {}

  ngOnInit(): void {
    this.registerForm = this._fb.group({
      userName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.pattern(
            '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,15}'
          ),
        ],
      ],
    });
  }

  get getUserName() {
    return this.registerForm.get('userName');
  }

  get getEmail() {
    return this.registerForm.get('email');
  }

  get getPassword() {
    return this.registerForm.get('password');
  }

  onUserRegister() {
    const regDetails = this.registerForm.value;
    console.log('form values', regDetails);
    if (this.registerForm.valid) {
      this._userService.register(regDetails).subscribe({
        next: (resp) => {
          if (resp) {
            console.log('response', resp);

            this.alert = true;
            this.alertClass = 'alert alert-success';
            this.successMessage = 'User Registered';
            setTimeout(() => {
              this._route.navigate(['login']);
            }, 600);
          } else {
            this.alert = true;
            this.alertClass = 'alert alert-danger';
            this.errorMessage = 'User already registered';
          }
        },
        error: (err) => {
          console.log('error', err);

          this.alert = true;
          this.alertClass = 'alert alert-danger';
          this.errorMessage = 'Something went wrong.';
          this.registerForm = this._fb.group({
            userName: '',
            email: new FormControl('', [Validators.required, Validators.email]),
            password: ['', Validators.required],
          });
        },
      });
    } else {
      this.ValidateFormFields(this.registerForm);
    }
  }

  closeAlert() {
    this.alert = false;
  }

  private ValidateFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.ValidateFormFields(control);
      }
    });
  }
}
