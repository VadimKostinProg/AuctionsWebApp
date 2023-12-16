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

  options: DataTableOptionsModel = {
    title: 'Categories',
    resourceName: 'category',
    showIndexColumn: true,
    showDeletedData: true,
    allowCreating: true,
    createFormOptions: {
      form: new FormGroup({
        name: new FormControl(null, [Validators.required]),
        description: new FormControl(null, [Validators.required])
      }),
      properties: [
        {
          label: 'Name',
          propName: 'name'
        },
        {
          label: 'Description',
          propName: 'description'
        }
      ],
    },
    allowEdit: true,
    editFormOptions: {
      form: new FormGroup({
        id: new FormControl(null, [Validators.required]),
        name: new FormControl(null, [Validators.required]),
        description: new FormControl(null, [Validators.required])
      }),
      properties: [
        {
          label: 'Id',
          propName: 'id'
        },
        {
          label: 'Name',
          propName: 'name'
        },
        {
          label: 'Description',
          propName: 'description'
        }
      ],
    },
    allowDelete: true,
    emptyListDisplayLabel: 'There list of categories is empty.',
    columnSettings: [
      {
        title: 'Name',
        dataPropName: 'name',
        isOrderable: true,
        width: 30
      },
      {
        title: 'Description',
        dataPropName: 'description',
        isOrderable: false,
        width: 50
      },
    ]
  };

  categoryForm: FormGroup;

  error: string;

  constructor(private readonly categoriesService: CategoriesService,
    private readonly deepLinkingService: DeepLinkingService) {

  }

  ngOnInit(): void {
    this.categoryForm = new FormGroup({
      id: new FormControl(null, [Validators.required]),
      username: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  onCreateCategory() {
    if (!this.options.createFormOptions.form.valid) {
      return;
    }
    var category = this.options.createFormOptions.form.value;

    this.categoriesService.createNewCategory(category).subscribe(
      async (response) => {
        // TODO: add toaster

        console.log(response);

        await this.dateTable.reloadDatatable();
      },
      (error) => {
        // TODO: add toaster
        console.log(error);
      }
    )

    this.options.createFormOptions.form = new FormGroup({
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required])
    });
  }

  getCategoriesApiUrl() {
    return this.categoriesService.getCategoriesApiUrl();
  }
}
