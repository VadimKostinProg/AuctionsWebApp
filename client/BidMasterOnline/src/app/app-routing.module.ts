import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './general/home/home.component';
import { SearchComponent } from './general/search/search.component';
import { SignInComponent } from './user-accounts/sign-in/sign-in.component';
import { CreateAccountComponent } from './user-accounts/create-account/create-account.component';
import { CategoriesComponent } from './admin/categories/categories.component';
import { CanActivateGuardService } from './services/can-activate-guard.service';
import { StaffManagementComponent } from './admin/staff-management/staff-management.component';
import { ConfirmEmailComponent } from './general/confirm-email/confirm-email.component';
import { ProfileComponent } from './user-accounts/profile/profile.component';
import { CustomersManagementComponent } from './technical-support/customers-management/customers-management.component';
import { CreateAuctionComponent } from './customer/create-auction/create-auction.component';
import { AuctionCreationRequestsListComponent } from './admin/auction-creation-requests-list/auction-creation-requests-list.component';
import { AuctionCreationRequestComponent } from './admin/auction-creation-request/auction-creation-request.component';
import { AuctionDetailsComponent } from './auctions/auction-details/auction-details.component';

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
    path: 'auction-details',
    component: AuctionDetailsComponent
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
    path: 'confirm-email',
    component: ConfirmEmailComponent,
    canActivate: [CanActivateGuardService],
    data: {
      expectedRoles: [
        'Admin',
        'TechnicalSupportSpecialist',
        'Customer'
      ]
    }
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [CanActivateGuardService],
    data: {
      expectedRoles: [
        'Admin',
        'TechnicalSupportSpecialist',
        'Customer'
      ]
    }
  },
  {
    path: 'staff-management',
    component: StaffManagementComponent,
    canActivate: [CanActivateGuardService],
    data: { expectedRoles: ['Admin'] }
  },
  {
    path: 'categories',
    component: CategoriesComponent,
    canActivate: [CanActivateGuardService],
    data: { expectedRoles: ['Admin'] }
  },
  {
    path: 'customers-management',
    component: CustomersManagementComponent,
    canActivate: [CanActivateGuardService],
    data: { expectedRoles: ['TechnicalSupportSpecialist'] }
  },
  {
    path: 'create-auction',
    component: CreateAuctionComponent,
    canActivate: [CanActivateGuardService],
    data: { expectedRoles: ['Customer'] }
  },
  {
    path: 'auction-creation-requests-list',
    component: AuctionCreationRequestsListComponent,
    canActivate: [CanActivateGuardService],
    data: { expectedRoles: ['Admin'] }
  },
  {
    path: 'auction-creation-request',
    component: AuctionCreationRequestComponent,
    canActivate: [CanActivateGuardService],
    data: { expectedRoles: ['Admin'] }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
