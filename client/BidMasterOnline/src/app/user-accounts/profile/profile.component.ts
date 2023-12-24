import { Component, OnInit, TemplateRef } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import { DeepLinkingService } from 'src/app/services/deep-linking.service';
import { AuthService } from 'src/app/services/auth.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ChangePasswordModel } from 'src/app/models/changePasswordModel';
import { ProfileModel } from 'src/app/models/profileModel';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
})
export class ProfileComponent implements OnInit {

  user: ProfileModel;

  ownProfile: boolean;

  changePasswordForm: FormGroup;

  constructor(private readonly usersService: UsersService,
    private readonly deepLinkingService: DeepLinkingService,
    private readonly authService: AuthService,
    private readonly modalService: NgbModal,
    private readonly router: Router,
    private readonly toastrService: ToastrService) {

  }

  async ngOnInit() {
    var userId = await this.deepLinkingService.getQueryParam('userId');

    if (userId == null || userId == this.authService.user.userId) {
      const currentAuthOptions = this.authService.user;
      userId = currentAuthOptions.userId;
      this.ownProfile = true;
    }

    this.usersService.getUserProfileById(userId).subscribe(
      (response) => {
        this.user = response;
      },
      (error) => {
        this.toastrService.error(error.error, 'Error');
      }
    );

    this.refreshChangePasswordForm();
  }

  refreshChangePasswordForm() {
    this.changePasswordForm = new FormGroup({
      currentPassword: new FormControl(null, [Validators.required]),
      newPassword: new FormControl(null, [Validators.required]),
      confirmedNewPassword: new FormControl(null, [Validators.required]),
    });
  }

  open(content: TemplateRef<any>) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' })
  }

  onChangePasswordSubmit(modal: any) {
    if (!this.changePasswordForm.valid) {
      return;
    }

    const changePasswordModel = this.changePasswordForm.value as ChangePasswordModel;

    this.usersService.changePassword(changePasswordModel).subscribe(
      (response) => {
        this.toastrService.success(response.message, 'Success');

        this.refreshChangePasswordForm();

        modal.close();
      },
      (error) => {
        this.toastrService.error(error.error, 'Error');
      }
    );
  }

  onDeleteAccountSubmit(modal: any) {
    modal.close();

    this.usersService.deleteOwnAccount().subscribe(
      (response) => {
        this.toastrService.success(response.message, 'Success');

        this.authService.logOut();
        this.router.navigate(['/']);
      },
      (error) => {
        this.toastrService.error(error.error, 'Error');
      }
    );
  }
}
