import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuctionsDeepLinkingService } from '../services/auctions-deep-linking.service';
import { AuctionsService } from '../services/auctions.service';
import { AuctionModel } from '../models/auctionModel';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent implements OnInit {
  placeholder = 'Search...';

  auctions: AuctionModel[];

  constructor(private readonly auctionsService: AuctionsService,
    private readonly auctionsDeepLinkingService: AuctionsDeepLinkingService) {
  }

  async ngOnInit(): Promise<void> {
    await this.onSearchOrFilterProceed();
  }

  async onSearchOrFilterProceed() {
    const queryParams = this.auctionsDeepLinkingService.getAllQueryParams();

    this.auctions = await this.auctionsService.getAuctions({ specifications: queryParams });
  }
}
