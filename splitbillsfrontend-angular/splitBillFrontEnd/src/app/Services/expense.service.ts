import {
  HttpClient,
  HttpHeaderResponse,
  HttpHeaders,
} from '@angular/common/http';

import { Injectable } from '@angular/core';
import ExpenseDTO from '../models/ExpenseDTO';
import GroupExpenseDTO from '../models/GroupExpenseDTO';
import ExpenseDetailsDTO from '../models/ExpenseDetailsDTO';
import UpdateExpenseDTO from '../models/UpdateExpenseDTO';

@Injectable({
  providedIn: 'root',
})
export class ExpenseService {
  URL = 'https://localhost:44373/api/GroupExpense';
  expenseURL = 'https://localhost:44373/api/UserExpense';

  constructor(private _httpClient: HttpClient) {}

  displayAllExpenses(GroupId: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.get<GroupExpenseDTO[]>(
      `${this.URL}/GetGroupExpense?GroupId=${GroupId}`,
      {
        headers: head,
      }
    );
  }

  addExpneseInGroup(expenseDTO: ExpenseDTO) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });
    return this._httpClient.post(`${this.URL}/AddExpenseInGroup`, expenseDTO, {
      headers: head,
      responseType: 'text',
    });
  }

  deleteExpense(expenseId: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.delete(
      `${this.URL}/DeleteExpense?groupId=${expenseId}`,
      { headers: head }
    );
  }

  displayTotalExpense(groupId: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.get(
      `${this.URL}/DisplayExpense?groupId=${groupId}`,
      {
        headers: head,
      }
    );
  }

  fetchParticularExpenseDetails(expenseId: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.get<ExpenseDetailsDTO[]>(
      `${this.expenseURL}/FetchExpenseById?expenseId=${expenseId}`,
      { headers: head }
    );
  }

  updateExpense(data: UpdateExpenseDTO) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.put(`${this.URL}/UpdateExpense`, data, {
      headers: head,
      responseType: 'text',
    });
  }
}
