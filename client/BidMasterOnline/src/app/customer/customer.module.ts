import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { CreateAuctionComponent } from './create-auction/create-auction.component';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [
    CreateAuctionComponent,
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
  ]
})
export class CustomerModule { }
