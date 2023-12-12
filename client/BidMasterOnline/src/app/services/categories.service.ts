import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { environment } from 'src/environments/environment';
import { CategoryModel } from '../models/categoryModel';
import { Observable, catchError, throwError } from 'rxjs';
import { CreateCategoryModel } from '../models/createCategoryModel';
import { ListModel } from '../models/listModel';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  baseUrl: string = `${environment.apiUrl}/api/v1/categories`;

  constructor(private readonly httpClient: HttpClient) { }

  getCategoriesApiUrl() {
    return this.baseUrl;
  }

  getAllCategories(): Observable<CategoryModel[]> {
    return this.httpClient.get<CategoryModel[]>(this.baseUrl);
  }

  getCategoriesList(specifications: Params): Observable<ListModel<CategoryModel>> {
    const params = new HttpParams({ fromObject: specifications });

    return this.httpClient.get<ListModel<CategoryModel>>(`${this.baseUrl}/list`, { params });
  }

  getDeletedCategoriesList(specifications: Params): Observable<ListModel<CategoryModel>> {
    const params = new HttpParams({ fromObject: specifications });

    return this.httpClient.get<ListModel<CategoryModel>>(`${this.baseUrl}/list/deleted`, { params });
  }

  getCategoryById(id: string) {
    return this.httpClient.get<CategoryModel[]>(`${this.baseUrl}/${id}`).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }

  createNewCategory(category: CreateCategoryModel) {
    return this.httpClient.post(this.baseUrl, category).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }

  updateCategory(category: CategoryModel) {
    return this.httpClient.put(this.baseUrl, category).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }

  deleteCategory(id: string) {
    return this.httpClient.delete(`${this.baseUrl}/${id}`).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }

  recoverCategory(id: string) {
    return this.httpClient.post(`${this.baseUrl}/${id}/recover`, {}).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }
}
