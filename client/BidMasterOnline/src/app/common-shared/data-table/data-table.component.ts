import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ActionClickedModel } from 'src/app/models/actionClickedModel';
import { DataTableOptionsModel } from 'src/app/models/dataTableOptionsModel';
import { ListModel } from 'src/app/models/listModel';
import { PaginationModel } from 'src/app/models/paginationModel';
import { SortingModel } from 'src/app/models/sortingModel';
import { DeepLinkingService } from 'src/app/services/deep-linking.service';

@Component({
  selector: 'data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss']
})
export class DataTableComponent implements OnInit, OnDestroy {

  @Input()
  options: DataTableOptionsModel;

  @Input()
  apiUrl: string;

  @Output()
  onCreate = new EventEmitter<void>();

  @Output()
  onAction = new EventEmitter<ActionClickedModel<any>>();

  // Real url to send request to
  actualUrl: string;

  tableData: ListModel<any>;

  isCurrentDataDeleted: boolean = false;

  sorting: SortingModel;

  pagination: PaginationModel;

  pageSizeOptions = [10, 25, 50, 75];

  constructor(
    private readonly deepLinkingService: DeepLinkingService,
    private readonly httpClient: HttpClient) {

  }

  async ngOnInit() {
    this.actualUrl = this.apiUrl;

    this.sorting = await this.deepLinkingService.getSortingParams();
    this.pagination = await this.deepLinkingService.getPaginationParams();

    if (this.pagination.pageNumber == null) {
      this.pagination = {
        pageNumber: 1,
        pageSize: 25
      } as PaginationModel;

      await this.deepLinkingService.setPaginationParams(this.pagination);
    }

    await this.reloadDatatable();
  }

  async reloadDatatable() {
    const specifications = await this.deepLinkingService.getAllQueryParams();
    const params = new HttpParams({ fromObject: specifications });

    this.httpClient.get(this.actualUrl, { params }).subscribe((data) => {
      this.tableData = data as ListModel<any>;
    });
  }

  onShowDeletedClick() {
    if (this.isCurrentDataDeleted) {
      this.isCurrentDataDeleted = false;

      this.actualUrl = this.apiUrl;
    } else {
      this.isCurrentDataDeleted = true;

      this.actualUrl = `${this.apiUrl}/deleted`;
    }
  }

  async decrementPageNumber() {
    this.pagination.pageNumber--;

    await this.deepLinkingService.setPaginationParams(this.pagination);

    await this.reloadDatatable();
  }

  async onPageNumberChanged(pageNumber: number) {
    this.pagination.pageNumber = pageNumber;

    await this.deepLinkingService.setPaginationParams(this.pagination);

    await this.reloadDatatable();
  }

  async incrementPageNumber() {
    this.pagination.pageNumber++;

    await this.deepLinkingService.setPaginationParams(this.pagination);

    await this.reloadDatatable();
  }

  async onPageSizeChanged(pageSize: any) {
    this.pagination.pageSize = pageSize.target.value.number;

    await this.deepLinkingService.setPaginationParams(this.pagination);

    await this.reloadDatatable();
  }

  onCreateClicked() {
    this.onCreate.emit();
  }

  onActionClicked(actionName: string, row: any) {
    var actionClickedModel = {
      actionName: actionName,
      actionObject: row
    } as ActionClickedModel<any>;

    this.onAction.emit(actionClickedModel);
  }

  async ngOnDestroy() {
    await this.deepLinkingService.clearPaginationParams();
    await this.deepLinkingService.clearSortingParams();
  }
}
