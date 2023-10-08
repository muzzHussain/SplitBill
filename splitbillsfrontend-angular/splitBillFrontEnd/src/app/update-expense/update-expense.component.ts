import { Component, OnInit } from '@angular/core';
import GroupDetailsDTO from '../models/GroupDetailsDTO';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ExpenseService } from '../Services/expense.service';
import { ActivatedRoute, Router } from '@angular/router';
import ExpenseDetailsDTO from '../models/ExpenseDetailsDTO';
import UpdateExpenseDTO from '../models/UpdateExpenseDTO';

@Component({
  selector: 'app-update-expense',
  templateUrl: './update-expense.component.html',
  styleUrls: ['./update-expense.component.css'],
})
export class UpdateExpenseComponent implements OnInit {
  expTitle: string = '';
  groupId: string = '';
  expId: string = '';
  alert: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  expenseDetails: ExpenseDetailsDTO[] = [];
  amountControls: { [userName: string]: FormControl } = {};
  expenseForms!: FormGroup;
  userControls: FormControl[] = [];

  constructor(
    private _expenseService: ExpenseService,
    private _router: Router,
    private _route: ActivatedRoute,
    private _fb: FormBuilder
  ) {}

  async ngOnInit() {
    this.expenseForms = new FormGroup({
      groupId: new FormControl('', []),
      expenseTitle: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
        Validators.pattern('[a-zA-Z].*'),
      ]),
      usersArray: this._fb.group({}),
    });
    this.expId = this._route.snapshot.params['id'];
    this.fetchExpenseDetails(this.expId);
  }

  addUserControl(userName: string, control: FormControl) {
    this.usersArray.addControl(userName, control);
  }

  get usersArray() {
    return this.expenseForms.get('usersArray') as FormGroup;
  }

  fetchExpenseDetails(expenseId: string) {
    this._expenseService.fetchParticularExpenseDetails(expenseId).subscribe(
      (resp) => {
        this.expenseDetails = resp;
        this.expTitle = this.expenseDetails[0].expenseTitle;
        this.groupId = this.expenseDetails[0].groupId;

        this.userControls = [];

        for (const group of this.expenseDetails) {
          const userControl = new FormControl(group.spendAmount);
          this.userControls.push(userControl);
          this.addUserControl(group.userName, userControl);
        }

        this.expenseForms.patchValue({
          groupId: this.groupId,
          expenseTitle: this.expTitle,
        });
      },
      (err) => {
        console.error('Error Occured', err);
      }
    );
  }

  onSubmit() {
    if (this.expenseForms.valid) {
      const groupIdValue = this.expenseForms.get('groupId')?.value;
      const titleValue = this.expenseForms.get('expenseTitle')?.value;
      if (groupIdValue !== undefined && groupIdValue !== null) {
        const expenseDTO: UpdateExpenseDTO = {
          ExpenseId: this.expId,
          Title: titleValue || '',
          UsersList: {},
        };
        for (const group of this.expenseDetails) {
          const userName = group.userName;
          const userEmailId = group.emailId;
          const spendAmount = this.usersArray.get(userName)?.value || 0;
          expenseDTO.UsersList[userEmailId] = spendAmount;
        }
        console.log('Update Expense DTO', expenseDTO);

        this._expenseService.updateExpense(expenseDTO).subscribe(
          (resp) => {
            console.log('Response', resp);
            this.alert = true;
            this.alertClass = 'alert alert-success';
            this.successMessage = resp;
            setTimeout(() => {
              this.alert = false;
              this._router.navigateByUrl(`/group/expense/${this.groupId}`);
            }, 1000);
          },
          (err) => {
            console.error('Error Occured', err);
          }
        );
      } else {
        console.log('Group id is undefined /or null');
      }
    }
  }

  get ExpenseTitle(): FormControl {
    return this.expenseForms.get('expenseTitle') as FormControl;
  }

  closeAlert() {
    this.alert = false;
  }
}
