import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { CategoriesService } from 'src/app/services/categories.service';
import { DataTableOptionsModel } from 'src/app/models/dataTableOptionsModel';
import { CategoryModel } from 'src/app/models/categoryModel';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DeepLinkingService } from 'src/app/services/deep-linking.service';
import { CreateCategoryModel } from 'src/app/models/createCategoryModel';
import { DataTableComponent } from 'src/app/common-shared/data-table/data-table.component';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent implements OnInit {

  @ViewChild(DataTableComponent)
  dateTable: DataTableComponent;

  options: DataTableOptionsModel;

  placeholder: string = 'Search category...';

  error: string;

  constructor(private readonly categoriesService: CategoriesService) {

  }

  ngOnInit(): void {
    this.options = this.categoriesService.getDataTableOptions();
  }

  onCreateCategory() {
    if (!this.options.createFormOptions.form.valid) {
      return;
    }
    var category = this.options.createFormOptions.form.value;

    this.categoriesService.createNewCategory(category).subscribe(
      async (response) => {
        // TODO: add toaster

        await this.dateTable.reloadDatatable();
      },
      (error) => {
        // TODO: add toaster
      }
    )

    this.options = this.categoriesService.getDataTableOptions();
  }

  onEditCategory() {
    if (!this.options.editFormOptions.form.valid) {
      return;
    }
    var category = this.options.editFormOptions.form.value;

    this.categoriesService.updateCategory(category).subscribe(
      async (response) => {
        // TODO: add toaster

        await this.dateTable.reloadDatatable();
      },
      (error) => {
        // TODO: add toaster
      }
    )
  }

  onDeleteCategory(categoryId: string) {
    this.categoriesService.deleteCategory(categoryId).subscribe(
      async (response) => {
        // TODO: add toaster

        await this.dateTable.reloadDatatable();
      },
      (error) => {
        // TODO: add toaster
      }
    );
  }

  async onSearchClicked() {
    await this.dateTable.reloadDatatable();
  }

  getCategoriesApiUrl() {
    return this.categoriesService.getCategoriesDataTableApiUrl();
  }
}
