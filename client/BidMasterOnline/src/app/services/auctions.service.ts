import { Injectable } from '@angular/core';
import { AuctionModel } from '../models/auctionModel';
import { AuctionSpecificationsModel } from '../models/auctionSpecificationsModel';

@Injectable({
  providedIn: 'root'
})
export class AuctionsService {

  constructor() { }

  async getAuctions(specifications: AuctionSpecificationsModel): Promise<AuctionModel[]> {
    return [];
  }

  async getPopularAuctions(): Promise<AuctionModel[]> {
    return [];
  }

  async getEndingAuctions(): Promise<AuctionModel[]> {
    return [];
  }
}
