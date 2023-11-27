import { Injectable } from '@angular/core';
import { AuctionModel } from '../models/auctionModel';
import { Params } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuctionsService {

  serverUrl: string = `${environment.apiUrl}/api/auctions`;

  constructor(private readonly httpClient: HttpClient) { }

  async getAuctions(specifications: Params): Promise<AuctionModel[]> {
    const params = new HttpParams({ fromObject: specifications });

    this.httpClient.get(this.serverUrl, { params }).subscribe(response => {
      console.log('Server Response:', response);
    });

    return [];
  }

  async getPopularAuctions(): Promise<AuctionModel[]> {
    return [];
  }

  async getEndingAuctions(): Promise<AuctionModel[]> {
    return [];
  }
}
