import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataTableOptionsModel } from 'src/app/models/dataTableOptionsModel';
import { FormInputTypeEnum } from 'src/app/models/formInputTypeEnum';
import { ListModel } from 'src/app/models/listModel';
import { PaginationModel } from 'src/app/models/paginationModel';
import { SortDirectionEnum } from 'src/app/models/sortDirectionEnum';
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
  onEdit = new EventEmitter<void>();

  @Output()
  onDelete = new EventEmitter<string>();

  tableData: ListModel<any>;

  choosenItem: any;

  SortDirectionEnum = SortDirectionEnum;

  FormInputTypeEnum = FormInputTypeEnum;

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

  onEditClick(item: any, modal: TemplateRef<any>) {
    this.options.editFormOptions?.properties.forEach(element => {
      this.options.editFormOptions?.form.controls[element.propName].setValue(item[element.propName]);
    });
    this.modalService.open(modal, { ariaLabelledBy: 'modal-basic-title' });
  }

  onDeleteClick(item: any, modal: TemplateRef<any>) {
    this.choosenItem = item;
    this.modalService.open(modal, { ariaLabelledBy: 'modal-basic-title' });
  }

  isTextType = (inputType: FormInputTypeEnum): boolean => inputType == FormInputTypeEnum.Text;
  isPasswordType = (inputType: FormInputTypeEnum): boolean => inputType == FormInputTypeEnum.Password;
  isSelectType = (inputType: FormInputTypeEnum): boolean => inputType == FormInputTypeEnum.Select;
  isDateType = (inputType: FormInputTypeEnum): boolean => inputType == FormInputTypeEnum.Date;

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

  async onSortingChanged(field: string) {
    if (this.sorting.sortField != field) {
      this.sorting.sortField = field;
      this.sorting.sortDirection = SortDirectionEnum.ASC;

      await this.deepLinkingService.setSortingParams(this.sorting);
    } else if (this.sorting.sortField == field && this.sorting.sortDirection == SortDirectionEnum.ASC) {
      this.sorting.sortDirection = SortDirectionEnum.DESC;

      await this.deepLinkingService.setSortingParams(this.sorting);
    } else {
      this.sorting.sortField = null;

      await this.deepLinkingService.clearSortingParams();
    }

    await this.reloadDatatable();
  }

  onCreateSubmit(modal: any) {
    modal.close();
    this.onCreate.emit();
  }

  onEditSubmit(modal: any) {
    modal.close();
    this.onEdit.emit();
  }

  onDeleteSubmit(modal: any) {
    modal.close();
    this.onDelete.emit(this.choosenItem.id);
  }
}
