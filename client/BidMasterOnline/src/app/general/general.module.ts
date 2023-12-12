import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { SearchComponent } from './search/search.component';
import { CommonSharedModule } from '../common-shared/common-shared.module';
import { AuctionsModule } from '../auctions/auctions.module';



@NgModule({
  declarations: [
    HomeComponent,
    SearchComponent,
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    AuctionsModule,
  ],
  exports: [
    HomeComponent,
    SearchComponent,
  ]
})
export class GeneralModule { }
