import { Injectable } from '@angular/core';
import { DeepLinkingService } from './deep-linking.service';

@Injectable({
  providedIn: 'root'
})
export class AuctionsDeepLinkingService extends DeepLinkingService {

  getCategory() {
    return super.getQueryParam('category');
  }

  async setCategory(value: string) {
    await super.setQueryParam('category', value);
  }

  async clearCategory() {
    await super.clearQueryParam('category');
  }

  getStartPriceDiapason() {
    return {
      min: super.getQueryParam('minStartPrice'),
      max: super.getQueryParam('maxStartPrice'),
    };
  }

  async setStartPriceDiapason(min: number, max: number) {
    await super.setQueryParam('minStartPrice', min);
    await super.setQueryParam('maxStartPrice', max);
  }

  async clearStartPriceDiapason() {
    await super.clearQueryParam('minStartPrice');
    await super.clearQueryParam('maxStartPrice');
  }

  getCurrentBidDiapason() {
    return {
      min: super.getQueryParam('minCurrentBid'),
      max: super.getQueryParam('maxCurrentBid'),
    };
  }

  async setCurrentBidDiapason(min: number, max: number) {
    await super.setQueryParam('minCurrentBid', min);
    await super.setQueryParam('maxCurrentBid', max);
  }

  async clearCurrentBidDiapason() {
    await super.clearQueryParam('minCurrentBid');
    await super.clearQueryParam('maxCurrentBid');
  }

  getStatus() {
    return super.getQueryParam('status');
  }

  async setStatus(value: string) {
    await super.setQueryParam('status', value);
  }

  async clearStatus(value: string) {
    await super.clearQueryParam('status');
  }
}