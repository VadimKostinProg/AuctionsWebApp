import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SetComplaintModel } from '../models/setComplaintModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComplaintsService {

  baseUrl: string = `${environment.apiUrl}/api/v1/technical-support/complaints`;

  constructor(private readonly httpClient: HttpClient) { }

  setComplaint(complaint: SetComplaintModel): Observable<any> {
    return this.httpClient.post(this.baseUrl, complaint);
  }
}
