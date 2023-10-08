import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginAndRegisterModule } from './login-and-register/login-and-register.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { IndivisualGroupModule } from './indivisual-group/indivisual-group.module';
import { NavbarComponent } from './navbar/navbar.component';
import { UserService } from './Services/userService';
import { HttpClientModule } from '@angular/common/http';
import { CreateGroupComponent } from './create-group/create-group.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ExpenseComponent } from './add-expense/expense.component';
import { SharedModule } from './shared/shared.module';
import { UpdateExpenseComponent } from './update-expense/update-expense.component';
import { DisplayExpenseComponent } from './display-expense/display-expense.component';
import { DisplayContributionComponent } from './display-contribution/display-contribution.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NavbarComponent,
    CreateGroupComponent,
    ExpenseComponent,
    DisplayExpenseComponent,
    UpdateExpenseComponent,
    DisplayContributionComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LoginAndRegisterModule,
    IndivisualGroupModule,
    HttpClientModule,
    SharedModule,
    ReactiveFormsModule,
  ],
  providers: [UserService],
  bootstrap: [AppComponent],
})
export class AppModule {}
