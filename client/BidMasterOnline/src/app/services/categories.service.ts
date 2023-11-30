import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { environment } from 'src/environments/environment';
import { CategoryModel } from '../models/categoryModel';
import { Observable, catchError, throwError } from 'rxjs';
import { CreateCategoryModel } from '../models/createCategoryModel';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  baseUrl: string = `${environment.apiUrl}/api/categories`;

  constructor(private readonly httpClient: HttpClient) { }

  GetCategoriesList(specifications: Params): Observable<CategoryModel[]> {
    const params = new HttpParams({ fromObject: specifications });

    return this.httpClient.get<CategoryModel[]>(this.baseUrl, { params });
  }

  GetDeletedCategoriesList(specifications: Params): Observable<CategoryModel[]> {
    const params = new HttpParams({ fromObject: specifications });

    return this.httpClient.get<CategoryModel[]>(`${this.baseUrl}/deleted`, { params });
  }

  GetCategoryById(id: string) {
    return this.httpClient.get<CategoryModel[]>(`${this.baseUrl}/${id}`).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }

  CreateNewCategory(category: CreateCategoryModel) {
    return this.httpClient.post(this.baseUrl, category).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }

  UpdateCategory(category: CategoryModel) {
    return this.httpClient.put(this.baseUrl, category).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }

  DeleteCategory(id: string) {
    return this.httpClient.delete(`${this.baseUrl}/${id}`).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }

  RecoverCategory(id: string) {
    return this.httpClient.post(`${this.baseUrl}/${id}/recover`, {}).pipe(
      catchError((error: HttpErrorResponse) => {
        // TODO: show modal window
        alert(error.message);
        return throwError(() => new Error(error.message));
      })
    );
  }
}
