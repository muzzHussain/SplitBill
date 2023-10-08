import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './Services/authGuard';
import { CreateGroupComponent } from './create-group/create-group.component';

import { DisplayExpenseComponent } from './display-expense/display-expense.component';

import { ExpenseComponent } from './add-expense/expense.component';
import { UpdateExpenseComponent } from './update-expense/update-expense.component';
import { DisplayContributionComponent } from './display-contribution/display-contribution.component';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'create/group',
    component: CreateGroupComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'group/expense/:id',
    component: DisplayExpenseComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'group/add/expense/:id',
    component: ExpenseComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'group/edit/expense/:id',
    component: UpdateExpenseComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'group/contribution/:id',
    component: DisplayContributionComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
