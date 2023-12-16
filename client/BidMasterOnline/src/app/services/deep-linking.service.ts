import { Injectable } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { SortDirectionEnum } from '../models/sortDirectionEnum';
import { PaginationModel } from '../models/paginationModel';
import { SortingModel } from '../models/sortingModel';

@Injectable({
  providedIn: 'root'
})
export class DeepLinkingService {

  constructor(private readonly route: ActivatedRoute,
    private readonly router: Router) {

  }

  getQueryParam(key: string) {
    return this.route.snapshot.queryParams[key] || null;
  }

  getAllQueryParams() {
    return this.route.snapshot.queryParams;
  }

  async setQueryParam(key: string, value: any) {
    const params = { ...this.route.snapshot.queryParams };
    params[key] = value;

    const navigationExtras: NavigationExtras = {
      queryParams: params,
      queryParamsHandling: 'merge',
    };

    await this.router.navigate([], navigationExtras);
  }

  async clearQueryParam(key: string) {
    const params = { ...this.route.snapshot.queryParams };
    params[key] = null;

    const navigationExtras: NavigationExtras = {
      queryParams: params,
      queryParamsHandling: 'merge',
    };

    await this.router.navigate([], navigationExtras);
  }

  async clearAllQueryParams() {
    const navigationExtras: NavigationExtras = {
      queryParams: [],
      queryParamsHandling: 'merge',
    };

    await this.router.navigate([], navigationExtras);
  }

  async getSortingParams(): Promise<SortingModel> {
    const sortField = await this.getQueryParam('sortField');
    const sortDirection = await this.getQueryParam('sortDirection') as SortDirectionEnum;

    return {
      sortField: sortField,
      sortDirection: sortDirection
    } as SortingModel;
  }

  async setSortingParams(sorting: SortingModel) {
    await this.setQueryParam('sortField', sorting.sortField);
    await this.setQueryParam('sortDirection', SortDirectionEnum[sorting.sortDirection]);
  }

  async clearSortingParams() {
    await this.clearQueryParam('sortField');
    await this.clearQueryParam('sortDirection');
  }

  async getPaginationParams(): Promise<PaginationModel> {
    const pageNumber = await this.getQueryParam('pageNumber');
    const pageSize = await this.getQueryParam('pageSize');

    return {
      pageNumber: pageNumber,
      pageSize: pageSize
    } as PaginationModel;
  }

  async setPaginationParams(pagination: PaginationModel) {
    await this.setQueryParam('pageNumber', pagination.pageNumber);
    await this.setQueryParam('pageSize', pagination.pageSize);
  }

  async clearPaginationParams() {
    await this.clearQueryParam('pageNumber');
    await this.clearQueryParam('pageSize');
  }
}
