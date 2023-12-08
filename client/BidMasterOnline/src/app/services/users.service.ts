import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateUserModel } from '../models/createUserModel';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl: string = `${environment.apiUrl}/api/users`;

  constructor(private readonly httpClient: HttpClient) { }

  createUser(user: CreateUserModel): Observable<string> {
    return this.httpClient.post<string>(this.baseUrl, user);
  }
}
