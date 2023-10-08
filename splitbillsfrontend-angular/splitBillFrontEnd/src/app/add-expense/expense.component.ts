import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GroupService } from '../Services/groupService';
import { UserService } from '../Services/userService';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

import { ActivatedRoute, Params, Route, Router } from '@angular/router';
import { ExpenseService } from '../Services/expense.service';
import ExpenseDTO from '../models/ExpenseDTO';
import GroupDetailsDTO from '../models/GroupDetailsDTO';

@Component({
  selector: 'app-expense',

  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.css'],
})
export class ExpenseComponent implements OnInit {
  groupDetails: GroupDetailsDTO[] = [];
  groupTitle: any;
  groupId: any;
  alert: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private _httpCline: HttpClient,
    private _groupService: GroupService,
    private _expenseService: ExpenseService,
    private _userService: UserService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  expenseForm = new FormGroup({
    groupId: new FormControl('', []),

    expenseTitle: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(100),
      Validators.pattern('[a-zA-Z].*'),
    ]),
    expenseAmount: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(100),
      Validators.pattern('[a-zA-Z].*'),
    ]),
  });

  async ngOnInit(): Promise<void> {
    this.groupId = this.route.snapshot.params['id'];
    this.fetchIndividualGroup(this.groupId);
    // this.route.params.subscribe((params: Params) => {
    //   this.groupId = params['groupId'];
    // });
    // console.log('params id', this.groupId);
  }

  fetchIndividualGroup(groupId: string) {
    this._groupService.getIndividualGroupDetails(groupId).subscribe({
      next: (resp) => {
        this.groupDetails = resp;
        this.groupTitle = this.groupDetails[0].groupName;
      },

      error: (err: any) => {
        console.error('Error', err);
      },
    });
  }

  getEmail(emailId: any): any {
    console.log('emails', emailId);
  }

  onSubmit(): void {
    const selectedGroupMembers = [];
    const expTitle = this.expenseForm.get('expenseTitle')?.value;
    for (const group of this.groupDetails) {
      for (const member of group.groupMembers) {
        const checkbox = document.getElementById(
          `checkbox-${member.name}`
        ) as HTMLInputElement;
        const amountInput = document.getElementById(
          `inputAmount-${member.name}`
        ) as HTMLInputElement;

        if (checkbox && checkbox.checked && amountInput) {
          const amount = parseFloat(amountInput.value);
          if (!isNaN(amount) && amount > 0) {
            selectedGroupMembers.push({
              emailId: member.emailId,
              amount: amount,
            });
          }
        }
      }
      const userList: { [userEmail: string]: number } = {};
      for (const user of selectedGroupMembers) {
        userList[user.emailId] = user.amount;
      }
      const expenseDTO: ExpenseDTO = {
        groupId: this.groupDetails[0].id,
        title: expTitle || '',
        usersList: userList,
      };
      this._expenseService.addExpneseInGroup(expenseDTO).subscribe(
        (resp) => {
          this.alert = true;
          this.alertClass = 'alert alert-success';
          this.successMessage = resp;
          setTimeout(() => {
            this.alert = false;
          }, 1000);
        },
        (err) => {
          console.log('error', err);

          this.alert = true;
          this.alertClass = 'alert alert-danger';
          this.successMessage = 'Something went wrong while adding expense';
          setTimeout(() => {
            this.alert = false;
          }, 1000);
        }
      );
    }
  }

  get ExpenseTitle(): FormControl {
    return this.expenseForm.get('expenseTitle') as FormControl;
  }

  closeAlert() {
    this.alert = false;
  }
}
