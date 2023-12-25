import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomersManagementComponent } from './customers-management/customers-management.component';
import { ComplaintsComponent } from './complaints/complaints.component';
import { TechnicalSupportRequestsListComponent } from './technical-support-requests-list/technical-support-requests-list.component';
import { CommonSharedModule } from '../common-shared/common-shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { ComplaintDetailsComponent } from './complain-details/complaint-details.component';
import { TechnicalSupportRequestComponent } from './technical-support-request/technical-support-request.component';

@NgModule({
  declarations: [
    CustomersManagementComponent,
    ComplaintsComponent,
    ComplaintDetailsComponent,
    TechnicalSupportRequestsListComponent,
    TechnicalSupportRequestComponent,
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    NgbModule,
    RouterModule,
    ToastrModule.forRoot(),
  ],
  exports: [
    CustomersManagementComponent,
    ComplaintsComponent,
    ComplaintDetailsComponent,
    TechnicalSupportRequestsListComponent,
    TechnicalSupportRequestComponent,
  ]
})
export class TechnicalSupportModule { }
