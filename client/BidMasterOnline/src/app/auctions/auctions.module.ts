import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuctionComponent } from './auction/auction.component';
import { AuctionFiltersComponent } from './auction-filters/auction-filters.component';
import { AuctionCardComponent } from './auction-card/auction-card.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSliderModule } from 'ngx-slider-v2';

@NgModule({
  declarations: [
    AuctionComponent,
    AuctionFiltersComponent,
    AuctionCardComponent,
  ],
  imports: [
    CommonModule,
    NgbModule,    
    NgxSliderModule,
  ],
  exports: [
    AuctionComponent,
    AuctionFiltersComponent,
    AuctionCardComponent
  ]
})
export class AuctionsModule { }
