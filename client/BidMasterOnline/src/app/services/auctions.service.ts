import { Injectable } from '@angular/core';
import { AuctionModel } from '../models/auctionModel';
import { Params } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { PublishAuctionModel } from '../models/publishAuctionModel';
import { ListModel } from '../models/listModel';

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

  getPopularAuctions(): AuctionModel[] {
    var list: AuctionModel[] = [];

    this.httpClient.get<ListModel<AuctionModel>>(`${this.baseUrl}/list?pageSize=10&pageNumber=1&sortField=popularity`).subscribe(
      (response) => {
        list = response.list;
      }
    );

    return list;
  }

  getFinishingAuctions(): AuctionModel[] {
    var list: AuctionModel[] = [];

    this.httpClient.get<ListModel<AuctionModel>>(`${this.baseUrl}/list?pageSize=10&pageNumber=1&sortField=dateAndTime`).subscribe(
      (response) => {
        list = response.list;
      }
    );

    return list;
  }

  publishAuction(auction: PublishAuctionModel): Observable<any> {
    var form = new FormData();

    for (const image of auction.images) {
      form.append(`images`, image);
    }

    form.append('name', auction.name);
    form.append('categoryId', auction.categoryId);
    form.append('lotDescription', auction.lotDescription);
    form.append('finishType', auction.finishType);
    form.append('auctionTime', auction.auctionTime);
    form.append('finishTimeInterval', auction.finishTimeInterval);
    form.append('startPrice', auction.startPrice.toString());

    return this.httpClient.post(`${this.baseUrl}`, form);
  }
}
