import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CategoryModel } from 'src/app/models/categoryModel';
import { AuctionsService } from 'src/app/services/auctions.service';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-create-auction',
  templateUrl: './create-auction.component.html'
})
export class CreateAuctionComponent implements OnInit {

  createAuctionForm: FormGroup;

  images: File[];

  categories: CategoryModel[];

  showFinishTimeInterval: boolean = false;

  auctionTimeError: string = null;

  error: string = null;

  constructor(private readonly auctionsService: AuctionsService,
    private readonly categoriesService: CategoriesService,
    private readonly toastrService: ToastrService,
    private readonly router: Router,
    private readonly spinnerService: NgxSpinnerService) {

  }

  ngOnInit(): void {
    this.createAuctionForm = new FormGroup({
      name: new FormControl(null, [Validators.required]),
      categoryId: new FormControl(null, [Validators.required]),
      lotDescription: new FormControl(null, [Validators.required]),
      finishType: new FormControl(null, [Validators.required]),
      auctionTimeDays: new FormControl(1, [Validators.required, Validators.min(1), Validators.max(7)]),
      auctionTimeHours: new FormControl(0, [Validators.required, Validators.min(0), Validators.max(23)]),
      finishTimeIntervalHours: new FormControl(1, [Validators.min(1), Validators.max(5)]),
      startPrice: new FormControl(null, [Validators.required, Validators.min(100), Validators.max(10e9)])
    });

    this.categoriesService.getAllCategories().subscribe(
      (response) => {
        this.categories = response;
      },
      (error) => {
        this.toastrService.error(error.error, 'Error');
      }
    );
  }

  get name() {
    return this.createAuctionForm.get('name');
  }

  get categoryId() {
    return this.createAuctionForm.get('categoryId');
  }

  get lotDescription() {
    return this.createAuctionForm.get('lotDescription');
  }

  get auctionTimeDays() {
    return this.createAuctionForm.get('auctionTimeDays');
  }

  get auctionTimeHours() {
    return this.createAuctionForm.get('auctionTimeHours');
  }

  get finishType() {
    return this.createAuctionForm.get('finishType');
  }

  get finishTimeIntervalHours() {
    return this.createAuctionForm.get('finishTimeIntervalHours');
  }

  get startPrice() {
    return this.createAuctionForm.get('startPrice');
  }

  onFinishTypeChange(finishType: any) {
    this.showFinishTimeInterval = finishType.target.value == 'IncreasingFinishTime';
  }

  onImagesChange(files: any) {
    this.images = files.target.files;
  }

  onSubmit() {
    if (!this.createAuctionForm.valid || !this.validateAuctionTime()) {
      return;
    }

    const formValue = this.createAuctionForm.value;

    const auction = {
      images: this.images,
      name: formValue.name,
      categoryId: formValue.categoryId,
      lotDescription: formValue.lotDescription,
      finishType: formValue.finishType,
      auctionTime: `${formValue.auctionTimeDays}.${formValue.auctionTimeHours}:0:0`,
      finishTimeInterval: `${formValue.finishTimeIntervalHours}:0:0`,
      startPrice: formValue.startPrice
    };

    this.spinnerService.show();

    this.auctionsService.publishAuction(auction).subscribe(
      (response) => {
        this.spinnerService.hide();

        this.toastrService.success(response.message, 'Success');

        this.router.navigate(['/']);
      },
      (error) => {
        this.spinnerService.hide();

        this.error = error.error;
      }
    );
  }

  validateAuctionTime(): boolean {
    if (Number.parseInt(this.auctionTimeDays.value) == 7 && Number.parseInt(this.auctionTimeHours.value) > 0) {
      this.auctionTimeError = 'Maximum auction time can be 1 week.';

      return false;
    }

    this.auctionTimeError = null;

    return true;
  }
}
