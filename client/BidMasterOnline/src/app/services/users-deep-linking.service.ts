import { Injectable } from '@angular/core';
import { DeepLinkingService } from './deep-linking.service';
import { NavigationExtras } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UsersDeepLinkingService extends DeepLinkingService {

  async setWinner(winnerId: string) {
    const params = { ...this.route.snapshot.queryParams };
    params['auctionistId'] = null;
    params['winnerId'] = winnerId;

    const navigationExtras: NavigationExtras = {
      queryParams: params,
      queryParamsHandling: 'merge',
    };

    await this.router.navigate([], navigationExtras);
  }

  async setAuctionist(auctionistId: string) {
    const params = { ...this.route.snapshot.queryParams };
    params['winnerId'] = null;
    params['auctionistId'] = auctionistId;

    const navigationExtras: NavigationExtras = {
      queryParams: params,
      queryParamsHandling: 'merge',
    };

    await this.router.navigate([], navigationExtras);
  }
}
