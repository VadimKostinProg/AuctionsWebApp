import { Component, OnInit } from '@angular/core';
import { CategoriesService } from 'src/app/services/categories.service';
import { DataTableOptionsModel } from 'src/app/models/dataTableOptionsModel';
import { ActionClickedModel } from 'src/app/models/actionClickedModel';
import { CategoryModel } from 'src/app/models/categoryModel';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent implements OnInit {

  options: DataTableOptionsModel = {
    title: 'Categories',
    showIndexColumn: true,
    showDeletedData: true,
    allowCreating: true,
    showActionsColumn: true,
    actionNames: [
      "Edit",
      "Delete"
    ],
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

  constructor(private readonly categoriesService: CategoriesService) {

  }

  ngOnInit(): void {
    this.categoryForm = new FormGroup({
      id: new FormControl(null, [Validators.required]),
      username: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  getCategoriesApiUrl() {
    return this.categoriesService.getCategoriesApiUrl();
  }

  onCreateCategory() {
    this.categoryForm = new FormGroup({
      id: new FormControl(null, [Validators.required]),
      username: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  get id() {
    return this.categoryForm.get('id');
  }

  get name() {
    return this.categoryForm.get('name');
  }

  get description() {
    return this.categoryForm.get('description');
  }

  onCreateSubmit() {

  }

  onActionForCategory(actionModel: ActionClickedModel<CategoryModel>) {

  }
}
