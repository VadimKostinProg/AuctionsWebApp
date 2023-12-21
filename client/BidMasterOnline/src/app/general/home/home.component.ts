import { Component, OnInit } from '@angular/core';
import { AuctionsService } from '../../services/auctions.service';
import { AuctionModel } from '../../models/auctionModel';
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
    this.popularAuctions = this.auctionsService.getPopularAuctions();
    this.finishingAuctions = this.auctionsService.getFinishingAuctions();
  }

  async onSearchPressed() {
    await this.router.navigate(['/search'], { queryParamsHandling: 'merge' });
  }
}
