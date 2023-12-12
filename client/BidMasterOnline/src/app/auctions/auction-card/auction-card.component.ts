import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuctionModel } from '../../models/auctionModel';

@Component({
  selector: 'auction-card',
  templateUrl: './auction-card.component.html',
  styleUrl: './auction-card.component.scss'
})
export class AuctionCardComponent {
  @Input()
  auction: AuctionModel;

  onDetailsClick() {
    window.location.replace(`/auction/${this.auction.id}`);
  }
}
