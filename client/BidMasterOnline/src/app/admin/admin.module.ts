import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesComponent } from './categories/categories.component';
import { CommonSharedModule } from '../common-shared/common-shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { StaffManagementComponent } from './staff-management/staff-management.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    CategoriesComponent,
    StaffManagementComponent,
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    ReactiveFormsModule,
    NgbModule,
    ToastrModule.forRoot(),
  ],
  exports: [
    CategoriesComponent,
    StaffManagementComponent,
  ]
})
export class AdminModule { }
