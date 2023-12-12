import { Injectable } from '@angular/core';
import { AuctionModel } from '../models/auctionModel';
import { Params } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuctionsService {

  baseUrl: string = `${environment.apiUrl}/api/v1/auctions`;

  constructor(private readonly httpClient: HttpClient) { }

  getAuctionsList(specifications: Params): Observable<AuctionModel[]> {
    const params = new HttpParams({ fromObject: specifications });

    return this.httpClient.get<AuctionModel[]>(this.baseUrl, { params });
  }

  getPopularAuctions(): Observable<AuctionModel[]> {
    return this.httpClient.get<AuctionModel[]>(`${this.baseUrl}/popular`);
  }

  getFinishingAuctions(): Observable<AuctionModel[]> {
    return this.httpClient.get<AuctionModel[]>(`${this.baseUrl}/finishing`);
  }
}
