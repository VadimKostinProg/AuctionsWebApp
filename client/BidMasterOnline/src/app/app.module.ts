import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AuctionComponent } from './auction/auction.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SearchBarComponent } from './common-components/search-bar/search-bar.component';
import { DataTableComponent } from './common-components/data-table/data-table.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AuctionComponent,
    LoginComponent,
    RegisterComponent,
    SearchBarComponent,
    DataTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
