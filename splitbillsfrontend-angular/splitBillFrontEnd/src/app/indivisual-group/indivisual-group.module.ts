import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionComponent } from './transaction/transaction.component';
import { MembersComponent } from './members/members.component';
import { HomeComponent } from './home/home.component';
import { IndivisualGroupRoutingModule } from './indivisual-group-routing.module';
import { CapitalizeFirstLetterPipe } from 'src/CustomeValidations&Pipe/capitalize-first-letter.pipe';

import { SharedModule } from '../shared/shared.module';
import { AddmembersComponent } from './addmembers/addmembers.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    TransactionComponent,
    MembersComponent,
    HomeComponent,
    AddmembersComponent,
  ],
  imports: [
    CommonModule,
    IndivisualGroupRoutingModule,
    ReactiveFormsModule,
    SharedModule,
  ],
})
export class IndivisualGroupModule {}
