import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LoginAndRegisterRoutingModule } from './login-and-register-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [
    CommonModule,
    LoginAndRegisterRoutingModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  exports:[
    LoginComponent,
    RegisterComponent
  ]
})
export class LoginAndRegisterModule {}
