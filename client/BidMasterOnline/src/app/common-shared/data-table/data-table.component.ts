import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
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
export class DataTableComponent implements OnInit {

  @Input()
  options: DataTableOptionsModel;

  @Input()
  apiUrl: string;

  @Output()
  onCreate = new EventEmitter<void>();

  @Output()
  onAction = new EventEmitter<ActionClickedModel<any>>();

  tableData: ListModel<any>;

  isCurrentDataDeleted: boolean = false;

  sorting: SortingModel;

  pagination: PaginationModel;

  pagesList: number[];
  pageSizeOptions = [10, 25, 50, 75];

  constructor(
    private readonly deepLinkingService: DeepLinkingService,
    private readonly httpClient: HttpClient,
    private readonly modalService: NgbModal) {

  }

  async ngOnInit() {
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

    this.httpClient.get(this.apiUrl, { params }).subscribe(async (data: any) => {
      this.tableData = {
        list: data.list,
        totalPages: data.totalPages
      };

      this.pagesList = [];

      for (let i = 1; i <= this.tableData.totalPages; i++) {
        this.pagesList.push(i);
      }

      if (this.pagination.pageNumber > this.tableData.totalPages) {
        await this.onPageNumberChanged(1);
      }
    });
  }

  open(content: TemplateRef<any>) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' })
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

  async onPageSizeChanged(pageSize: number) {
    this.pagination.pageSize = pageSize;

    await this.deepLinkingService.setPaginationParams(this.pagination);

    await this.reloadDatatable();
  }

  onCreateSubmit(content: any) {
    content.close();
    this.onCreate.emit();
  }
}
