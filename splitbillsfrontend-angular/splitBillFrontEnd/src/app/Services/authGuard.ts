import { Injectable } from '@angular/core';
import { UserService } from './userService';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private _userService: UserService, private _router: Router) {}

  canActivate() {
    if (this._userService.isLoggedIn()) {
      return true;
    } else {
      this._router.navigate(['login']);
      return false;
    }
  }
}
