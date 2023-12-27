import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuctionFiltersComponent } from './auction-filters/auction-filters.component';
import { AuctionCardComponent } from './auction-card/auction-card.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSliderModule } from 'ngx-slider-v2';
import { RouterModule } from '@angular/router';
import { AuctionDetailsComponent } from './auction-details/auction-details.component';
import { CommonSharedModule } from '../common-shared/common-shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CommentsComponent } from './comments/comments.component';
import { ScoreComponent } from './score/score.component';

@NgModule({
  declarations: [
    AuctionFiltersComponent,
    AuctionCardComponent,
    AuctionDetailsComponent,
    CommentsComponent,
    ScoreComponent
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    NgbModule,
    NgxSliderModule,
    RouterModule,
    ReactiveFormsModule,
  ],
  exports: [
    AuctionFiltersComponent,
    AuctionCardComponent,
    AuctionDetailsComponent,
    CommentsComponent,
    ScoreComponent
  ]
})
export class AuctionsModule { }
