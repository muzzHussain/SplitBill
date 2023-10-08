import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private tokenKey = 'jwt';
  private roleKey = 'role';

  constructor() {}

  getItem() {
    return localStorage.getItem(this.tokenKey);
  }

  setItem(token: any) {
    return localStorage.setItem(this.tokenKey, token);
  }

  removeItem() {
    return localStorage.removeItem(this.tokenKey);
  }

  removeRole() {
    return localStorage.removeItem(this.roleKey);
  }
}
