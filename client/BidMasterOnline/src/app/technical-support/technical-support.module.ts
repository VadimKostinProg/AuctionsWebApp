import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomersManagementComponent } from './customers-management/customers-management.component';
import { ComplaintsComponent } from './complaints/complaints.component';
import { TechnicalSupportRequestsComponent } from './technical-support-requests/technical-support-requests.component';
import { CommonSharedModule } from '../common-shared/common-shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    CustomersManagementComponent,
    ComplaintsComponent,
    TechnicalSupportRequestsComponent,
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    NgbModule,
  ],
  exports: [
    CustomersManagementComponent,
    ComplaintsComponent,
    TechnicalSupportRequestsComponent,
  ]
})
export class TechnicalSupportModule { }
