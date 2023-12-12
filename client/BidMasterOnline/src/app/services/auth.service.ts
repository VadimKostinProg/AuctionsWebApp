import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SignInModel } from '../models/signInModel';
import { AuthenticationModel } from '../models/authenticationModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl: string = `${environment.apiUrl}/api/auth`;

  userIdKey = 'user-id';
  tokenKey = 'token';
  roleKey = 'role';

  constructor(private readonly httpClient: HttpClient) { }

  signIn(signInModel: SignInModel): Observable<AuthenticationModel> {
    return this.httpClient.post<AuthenticationModel>(`${this.baseUrl}/login`, signInModel)
      .pipe(
        tap(response => {
          sessionStorage.setItem(this.userIdKey, response.userId);
          sessionStorage.setItem(this.tokenKey, response.token);
          sessionStorage.setItem(this.roleKey, response.role);
        }),
        catchError(error => {
          throw new Error(error);
        })
      );
  }

  getAuthenticatedUserId() {
    return sessionStorage.getItem(this.userIdKey);
  }

  getToken() {
    return sessionStorage.getItem(this.tokenKey);
  }

  getAuthenticatedUserRole() {
    return sessionStorage.getItem(this.roleKey);
  }

  logOut() {
    sessionStorage.removeItem(this.userIdKey);
    sessionStorage.removeItem(this.tokenKey);
    sessionStorage.removeItem(this.roleKey);
  }
}
