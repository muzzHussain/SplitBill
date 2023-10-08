import { Component, OnInit } from '@angular/core';
import ExpenseDetailsDTO from '../models/ExpenseDetailsDTO';
import { ExpenseService } from '../Services/expense.service';
import { ActivatedRoute } from '@angular/router';
import SettlementDTO from '../models/settlementDTO';

@Component({
  selector: 'app-display-contribution',
  templateUrl: './display-contribution.component.html',
  styleUrls: ['./display-contribution.component.css'],
})
export class DisplayContributionComponent implements OnInit {
  expId: string = '';
  alert: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  groupId: string = '';
  contribution: ExpenseDetailsDTO[] = [];
  textColor: string = '';
  settlementDTO: SettlementDTO[] = [];
  noAmt: boolean = false;
  isPay: boolean = false;
  eligibleToPay: string[] = [];

  constructor(
    private _expenseService: ExpenseService,
    private _route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.expId = this._route.snapshot.params['id'];
    this.fetchContribution(this.expId);
  }

  fetchContribution(expenseId: string) {
    this._expenseService
      .fetchParticularExpenseDetails(expenseId)
      .subscribe((resp) => {
        this.contribution = resp;
        this.groupId = this.contribution[0].groupId;
        this.whomToPay(this.contribution);
      });
  }

  whomToPay(data: ExpenseDetailsDTO[]) {
    var countUserWhoReceive = this.countUserWithGreaterSpentAmt(data);

    var countUserWhoHaveToPay = this.countUserWithLessSpentAmt(data);

    const settlementArray: SettlementDTO[] = [];
    const eligibleToPay: string[] = [];

    if (countUserWhoReceive == 0) {
      this.noAmt = false;
      this.isPay = false;
    } else if (countUserWhoReceive == 1) {
      this.isPay = true;
      this.noAmt = true;
      var namesToPay: string[] = [];
      var namesToReceive: string[] = [];

      for (var item of data) {
        if (item.spendAmount > item.perPerson)
          namesToReceive.push(item.userName);
        else if (item.spendAmount < item.perPerson) {
          namesToPay.push(item.userName);
          eligibleToPay.push(item.userName);
        }
      }

      for (var nameToPay of namesToPay) {
        for (var nameToReceive of namesToReceive) {
          const payer = data.find((item) => item.userName == nameToPay);
          const receiver = data.find((item) => item.userName == nameToReceive);
          if (payer && receiver) {
            const settlementItem: SettlementDTO = {
              needToPay: nameToPay,
              amount: Math.abs(payer?.spendAmount - payer?.perPerson),
              whomToPay: nameToReceive,
            };
            settlementArray.push(settlementItem);
          }
        }
      }
    } else {
      this.noAmt = true;
      for (var item of data) {
        if (item.spendAmount < item.perPerson) {
          const settlementAmt = Math.abs(item.spendAmount - item.perPerson);
          const perSettlementAmt = settlementAmt / countUserWhoReceive;
          while (countUserWhoHaveToPay != 0) {
            for (var amt of data) {
              if (amt.spendAmount > item.spendAmount) {
                const settlementItem: SettlementDTO = {
                  needToPay: item.userName,
                  amount: perSettlementAmt,
                  whomToPay: amt.userName,
                };
                settlementArray.push(settlementItem);
              }
            }
            countUserWhoHaveToPay--;
          }
          eligibleToPay.push(item.userName);
        }
      }
    }
    this.settlementDTO = settlementArray;
    this.eligibleToPay = eligibleToPay;
  }

  countUserWithLessSpentAmt(data: ExpenseDetailsDTO[]) {
    var cnt = 0;
    for (var item of data) {
      if (item.spendAmount < item.perPerson) cnt = cnt + 1;
    }
    return cnt;
  }

  countUserWithGreaterSpentAmt(data: ExpenseDetailsDTO[]) {
    var cnt = 0;
    for (var item of data) {
      if (item.spendAmount > item.perPerson) cnt = cnt + 1;
    }
    return cnt;
  }

  isEligibleToPay(item: ExpenseDetailsDTO): boolean {
    return item.spendAmount < item.perPerson;
  }

  generateQR() {
    alert('QR Generated');
  }

  closeAlert() {
    this.alert = false;
  }
}
