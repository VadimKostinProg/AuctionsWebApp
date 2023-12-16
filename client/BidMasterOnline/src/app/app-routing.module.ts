import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './general/home/home.component';
import { SearchComponent } from './general/search/search.component';
import { SignInComponent } from './user-accounts/sign-in/sign-in.component';
import { CreateAccountComponent } from './user-accounts/create-account/create-account.component';
import { CategoriesComponent } from './admin/categories/categories.component';
import { CanActivateGuardService } from './services/can-activate-guard.service';

const routes: Routes = [
  { 
    path: '', 
    component: HomeComponent 
  },
  { 
    path: 'search', 
    component: SearchComponent 
  },
  { 
    path: 'sign-in', 
    component: SignInComponent 
  },
  { 
    path: 'create-account', 
    component: CreateAccountComponent 
  },
  { 
    path: 'categories', 
    component: CategoriesComponent, 
    canActivate: [ CanActivateGuardService ],
    data: { expectedRole: 'Admin' } 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
