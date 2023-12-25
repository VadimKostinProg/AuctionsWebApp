import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { CreateAuctionComponent } from './create-auction/create-auction.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { TechnicalSupportComponent } from './technical-support/technical-support.component';

@NgModule({
  declarations: [
    CreateAuctionComponent,
    TechnicalSupportComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    NgbModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule.forRoot(),
  ],
  exports: [
    CreateAuctionComponent,
    TechnicalSupportComponent,
  ]
})
export class CustomerModule { }
