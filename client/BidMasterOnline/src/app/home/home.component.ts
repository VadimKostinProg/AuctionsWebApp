import { Component, OnInit } from '@angular/core';
import { AuctionsService } from '../services/auctions.service';
import { AuctionModel } from '../models/auctionModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  placeholder: string = 'Search auction...';

  popularAuctions: AuctionModel[];
  endingAuctions: AuctionModel[];

  constructor(private readonly auctionsService: AuctionsService) {

  }
  async ngOnInit(): Promise<void> {
    this.popularAuctions = await this.auctionsService.getPopularAuctions();
    this.endingAuctions = await this.auctionsService.getEndingAuctions();
  }

  onSearchPressed(searchTerm: string) {
    alert(searchTerm);
  }
}
