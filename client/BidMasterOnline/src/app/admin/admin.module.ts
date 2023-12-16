import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesComponent } from './categories/categories.component';
import { CommonSharedModule } from '../common-shared/common-shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    CategoriesComponent
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    ReactiveFormsModule,
    NgbModule
  ],
  exports: [
    CategoriesComponent
  ]
})
export class AdminModule { }
