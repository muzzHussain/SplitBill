import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/userService';
import { TokenService } from '../Services/tokenService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isLoggesIn = false;

  constructor(
    private router: Router,
    private userService: UserService,
    private tokenService: TokenService
  ) {}
  ngOnInit(): void {}

  onLogout() {
    this.userService.logout();
  }

  get isAuthenticated(): boolean {
    return this.userService.isLoggedIn();
  }
}
