import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesComponent } from './categories/categories.component';
import { CommonSharedModule } from '../common-shared/common-shared.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    CategoriesComponent
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    ReactiveFormsModule,
  ],
  exports: [
    CategoriesComponent
  ]
})
export class AdminModule { }
