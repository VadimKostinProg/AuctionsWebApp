import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AuctionComponent } from './auction/auction.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SearchBarComponent } from './common-components/search-bar/search-bar.component';
import { DataTableComponent } from './common-components/data-table/data-table.component';
import { AuctionFiltersComponent } from './common-components/auction-filters/auction-filters.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuctionCardComponent } from './auction-card/auction-card.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AuctionComponent,
    LoginComponent,
    RegisterComponent,
    SearchBarComponent,
    DataTableComponent,
    AuctionFiltersComponent,
    AuctionCardComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
