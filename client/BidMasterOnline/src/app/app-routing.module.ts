import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SearchComponent } from './search/search.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { CreateAccountComponent } from './create-account/create-account.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'search', component: SearchComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'create-account', component: CreateAccountComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
