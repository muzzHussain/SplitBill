import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenService } from './tokenService';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  URL = 'https://localhost:44373/api/User';

  constructor(
    private _httpClient: HttpClient,
    private _tokenService: TokenService
  ) {}

  register(user: any) {
    return this._httpClient.post(`${this.URL}/Register`, user);
  }

  async login(emailId: string, password: string) {
    try {
      const head = new HttpHeaders()
        .set('Content-Type', 'application/json')
        .set('emailId', emailId)
        .set('password', password);

      const resp: any = await this._httpClient
        .get(`${this.URL}/LoginUser`, { headers: head, responseType: 'text' })
        .toPromise();

      const decodeToken = this.decodeToken(resp);

      this._tokenService.setItem(resp);
    } catch (error) {
      throw error;
    }
  }

  async logout() {
    await this._tokenService.removeItem();
    await this._tokenService.removeRole();
  }

  async getUserName(): Promise<string> {
    const head = new HttpHeaders({
      'Content-Type': 'application/text',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    try {
      const resp = await this._httpClient
        .get<string>(`${this.URL}/UserName`, {
          headers: head,
          responseType: 'text' as 'json',
        })
        .toPromise();

      return resp as string;
    } catch (err) {
      console.error('Error', err);
      throw err;
    }
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('jwt');
    return token !== null && token !== undefined;
  }

  private decodeToken(token: string) {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch (err) {
      console.log('Invalid token', err);
      return null;
    }
  }
}
