import { Component, OnInit } from '@angular/core';
import { AuctionsService } from '../services/auctions.service';
import { AuctionModel } from '../models/auctionModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  placeholder: string = 'Search auction...';

  popularAuctions: AuctionModel[] = [];
  finishingAuctions: AuctionModel[] = [];

  constructor(private readonly auctionsService: AuctionsService,
    private readonly router: Router) {
  }
  ngOnInit() {
    this.auctionsService.getPopularAuctions().subscribe((auctions) => {
      this.popularAuctions = auctions;
    });
    this.auctionsService.getFinishingAuctions().subscribe((auctions) => {
      this.finishingAuctions = auctions;
    });
  }

  async onSearchPressed() {
    await this.router.navigate(['/search'], { queryParamsHandling: 'merge' });
  }
}
