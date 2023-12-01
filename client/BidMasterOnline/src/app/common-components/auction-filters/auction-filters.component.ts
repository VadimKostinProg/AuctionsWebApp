import { Component, OnInit, Output, TemplateRef } from '@angular/core';
import { ChangeContext, Options } from 'ngx-slider-v2';
import { CategoryModel } from 'src/app/models/categoryModel';
import { AuctionsDeepLinkingService } from 'src/app/services/auctions-deep-linking.service';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'auction-filters',
  templateUrl: './auction-filters.component.html',
  styleUrl: './auction-filters.component.scss'
})
export class AuctionFiltersComponent implements OnInit {

  defaultSliderMin = 100;
  defaultSliderMax = 400;

  minStartPrice: number;
  maxStartPrice: number;

  startPriceChanged = false;

  minCurrentBid: number;
  maxCurrentBid: number;

  currentBidChanged = false;

  categories: CategoryModel[];

  chosenCategoryId: string;

  chosenStatus: string;

  options: Options = {
    floor: 0,
    ceil: 500,
    translate: (value: number): string => {
      return '$' + value;
    }
  };

  constructor(private readonly auctionsDeeplinkingService: AuctionsDeepLinkingService,
    private readonly categoriesService: CategoriesService) {

  }

  async ngOnInit(): Promise<void> {
    // this.categoriesService.GetCategoriesList({ sortField: 'Name' }).subscribe((categories) => {
    //   this.categories = categories;
    // });

    this.categories = [
      {
        id: "id1",
        name: "Category1",
        description: "Description1",
      },
      {
        id: "id2",
        name: "Category2",
        description: "Description2",
      },
      {
        id: "id3",
        name: "Category3",
        description: "Description3",
      },
      {
        id: "id4",
        name: "Category4",
        description: "Description4",
      }
    ];

    const category = await this.auctionsDeeplinkingService.getCategoryId();
    const startPrice = await this.auctionsDeeplinkingService.getStartPriceDiapason();
    const currentBid = await this.auctionsDeeplinkingService.getCurrentBidDiapason();
    const status = await this.auctionsDeeplinkingService.getStatus();

    this.chosenCategoryId = category || '';

    this.minStartPrice = startPrice?.min || this.defaultSliderMin;
    this.maxStartPrice = startPrice?.max || this.defaultSliderMax;

    this.minCurrentBid = currentBid?.min || this.defaultSliderMin;
    this.maxCurrentBid = currentBid?.max || this.defaultSliderMax;

    this.chosenStatus = status || '';
  }

  async onCategoryChanged(category: any) {
    const value = category.target.value;

    if (this.chosenCategoryId == value) {
      this.chosenCategoryId = '';

      await this.auctionsDeeplinkingService.clearCategoryId();
    } else {
      this.chosenCategoryId = value;

      await this.auctionsDeeplinkingService.setCategoryId(value);
    }
  }

  async onStartPriceFilterChange(changeContext: ChangeContext) {
    this.startPriceChanged = true;

    await this.auctionsDeeplinkingService.setStartPriceDiapason(this.minStartPrice, this.maxStartPrice);
  }

  async clearStartPrice() {
    this.minStartPrice = this.defaultSliderMin;
    this.maxStartPrice = this.defaultSliderMax;

    this.startPriceChanged = false;

    await this.auctionsDeeplinkingService.clearStartPriceDiapason();
  }

  async onCurrentBidFilterChange(changeContext: ChangeContext) {
    this.currentBidChanged = true;

    await this.auctionsDeeplinkingService.setCurrentBidDiapason(this.minCurrentBid, this.maxCurrentBid);
  }

  async clearCurrentBid() {
    this.minCurrentBid = this.defaultSliderMin;
    this.maxCurrentBid = this.defaultSliderMax;

    this.currentBidChanged = false;

    await this.auctionsDeeplinkingService.clearCurrentBidDiapason();
  }

  async onStatusChanged(status: any) {
    const value = status.target.value;

    if (this.chosenStatus == value) {
      this.chosenStatus = '';

      await this.auctionsDeeplinkingService.clearStatus();
    } else {
      this.chosenStatus = value;

      await this.auctionsDeeplinkingService.setStatus(value);
    }
  }
}
