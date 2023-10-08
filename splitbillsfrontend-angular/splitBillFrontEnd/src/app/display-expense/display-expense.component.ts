import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ExpenseService } from '../Services/expense.service';
import GroupExpenseDTO from '../models/GroupExpenseDTO';

@Component({
  selector: 'app-display-expense',
  templateUrl: './display-expense.component.html',
  styleUrls: ['./display-expense.component.css'],
})
export class DisplayExpenseComponent implements OnInit {
  totalExpense!: any;
  alert: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  // expenseDetails: any[] = [];
  expenseDetails: GroupExpenseDTO[] = [];
  groupId: string = ' ';
  name = '';
  detail: GroupExpenseDTO[] = [];
  constructor(
    private route: ActivatedRoute,
    private expenseService: ExpenseService
  ) {}
  async ngOnInit(): Promise<void> {
    this.groupId = this.route.snapshot.params['id'];
    this.DisplayAllExpense(this.groupId);
    this.TotalExpense(this.groupId);
  }

  DisplayAllExpense(groupId: string) {
    this.expenseService.displayAllExpenses(groupId).subscribe((resp) => {
      this.detail = resp;
    });
  }

  TotalExpense(groupId: string) {
    this.expenseService.displayTotalExpense(groupId).subscribe({
      next: (resp) => {
        this.totalExpense = resp;
      },
      error: (err) => {
        console.log('error occured', err);
      },
    });
  }

  onDelete(expenseId: string) {
    this.expenseService.deleteExpense(expenseId).subscribe(
      (resp) => {
        if (resp) {
          this.alert = true;
          this.alertClass = 'alert alert-success';
          this.successMessage = 'Successfully Deleted';
          setTimeout(() => {
            this.alert = false;
            this.DisplayAllExpense(this.groupId);
            this.TotalExpense(this.groupId);
          }, 800);
        }
      },
      (err) => {
        console.log('error', err);
        this.alert = true;
        this.alertClass = 'alert alert-danger';
        this.errorMessage = 'Something went wrong';
        setTimeout(() => {
          this.alert = false;
        }, 800);
      }
    );
  }

  onUpdate(expenseId: string) {
    // this.expenseService.fetchParticularExpenseDetails(expenseId).subscribe(
    //   (resp) => {
    //     console.log('Expense Details', resp);
    //   },
    //   (err) => {
    //     console.error('Error Occured', err);
    //   }
    // );
  }

  closeAlert() {
    this.alert = false;
  }
}
