import { Component, OnInit, Output, TemplateRef } from '@angular/core';
import { ChangeContext, Options } from 'ngx-slider-v2';
import { AuctionsDeepLinkingService } from 'src/app/services/auctions-deep-linking.service';

@Component({
  selector: 'auction-filters',
  templateUrl: './auction-filters.component.html',
  styleUrl: './auction-filters.component.scss'
})
export class AuctionFiltersComponent implements OnInit {

  minStartPrice = 100;
  maxStartPrice = 400;

  minCurrentBid = 100;
  maxCurrentBid = 400;

  categories: any[];

  options: Options = {
    floor: 0,
    ceil: 500,
    translate: (value: number): string => {
      return '$' + value;
    }
  };

  constructor(private readonly auctionsDeeplinkingService: AuctionsDeepLinkingService) {

  }

  async ngOnInit(): Promise<void> {
    const startPrice = await this.auctionsDeeplinkingService.getStartPriceDiapason();
    const currentBid = await this.auctionsDeeplinkingService.getCurrentBidDiapason();

    this.minStartPrice = startPrice?.min || 100;
    this.maxStartPrice = startPrice?.max || 400;

    this.minCurrentBid = currentBid?.min || 100;
    this.maxCurrentBid = currentBid?.max || 400;
  }

  async onCategoryChanged(category: any) {
    await this.auctionsDeeplinkingService.setStatus(category.target.value);
  }

  async onStartPriceFilterChange(changeContext: ChangeContext) {
    await this.auctionsDeeplinkingService.setStartPriceDiapason(this.minStartPrice, this.maxStartPrice);
  }

  async onCurrentBidFilterChange(changeContext: ChangeContext) {
    await this.auctionsDeeplinkingService.setCurrentBidDiapason(this.minCurrentBid, this.maxCurrentBid);
  }

  async onStatusChanged(status: any) {
    await this.auctionsDeeplinkingService.setStatus(status.target.value);
  }
}
