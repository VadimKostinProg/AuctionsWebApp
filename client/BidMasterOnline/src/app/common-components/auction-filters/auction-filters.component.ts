import { ChangeContext, Options } from '@angular-slider/ngx-slider';
import { Component } from '@angular/core';
import { AuctionsDeepLinkingService } from 'src/app/services/auctions-deep-linking.service';

@Component({
  selector: 'auction-filters',
  templateUrl: './auction-filters.component.html',
  styleUrl: './auction-filters.component.scss'
})
export class AuctionFiltersComponent {

  minStartPrice: number = 100;
  maxStartPrice: number = 400;

  minCurrentBid: number = 100;
  maxCurrentBid: number = 400;

  options: Options = {
    floor: 0,
    ceil: 500,
    translate: (value: number): string => {
      return '$' + value;
    }
  };

  constructor(private readonly auctionsDeeplinkingService: AuctionsDeepLinkingService) {

  }

  async onStartPriceFilterChange(changeContext: ChangeContext) {
    await this.auctionsDeeplinkingService.setStartPriceDiapason(this.minStartPrice, this.maxStartPrice);
  }

  async onCurrentBidFilterChange(changeContext: ChangeContext) {
    await this.auctionsDeeplinkingService.setCurrentBidDiapason(this.minCurrentBid, this.maxCurrentBid);
  }
}
