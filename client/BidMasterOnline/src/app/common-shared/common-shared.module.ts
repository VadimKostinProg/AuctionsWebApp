import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchBarComponent } from './search-bar/search-bar.component';
import { DataTableComponent } from './data-table/data-table.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    SearchBarComponent,
    DataTableComponent,
  ],
  imports: [
    CommonModule,
    NgSelectModule,
    FormsModule,
  ],
  exports: [
    SearchBarComponent,
    DataTableComponent,
  ]
})
export class CommonSharedModule { }
