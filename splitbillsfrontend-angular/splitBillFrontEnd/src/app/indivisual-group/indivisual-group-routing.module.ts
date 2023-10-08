import { Component, NgModule, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { TransactionComponent } from './transaction/transaction.component';
import { AuthGuard } from '../Services/authGuard';
import { AddmembersComponent } from './addmembers/addmembers.component';

const routes: Routes = [
  {
    path: 'group/members/:id',
    component: MembersComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'group/home',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'group/transactions',
    component: TransactionComponent,
    canActivate: [AuthGuard],
  },

  {
    path: 'group/add/members/:id',
    component: AddmembersComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes), CommonModule],
  exports: [RouterModule],
})
export class IndivisualGroupRoutingModule {}
