import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateUserModel } from '../models/createUserModel';
import { UserRoleEnum } from '../models/userRoleEnum';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl: string = `${environment.apiUrl}/api/v1/users`;

  constructor(private readonly httpClient: HttpClient) { }

  createCustomer(user: CreateUserModel): Observable<string> {
    return this.httpClient.post<string>(`${this.baseUrl}/customers`, user);
  }
}
