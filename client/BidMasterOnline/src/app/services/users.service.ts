import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateUserModel } from '../models/createUserModel';
import { UserRoleEnum } from '../models/userRoleEnum';
import { DataTableOptionsModel } from '../models/dataTableOptionsModel';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormInputTypeEnum } from '../models/formInputTypeEnum';
import { ChangePasswordModel } from '../models/changePasswordModel';
import { ProfileModel } from '../models/profileModel';
import { OptionalActionModel } from '../models/optionalActionModel';
import { BlockUserModel } from '../models/blockUserModel';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl: string = `${environment.apiUrl}/api/v1/users`;

  constructor(private readonly httpClient: HttpClient) { }

  getUserProfileById(userId: string): Observable<ProfileModel> {
    return this.httpClient.get<ProfileModel>(`${this.baseUrl}/${userId}`);
  }

  createCustomer(user: CreateUserModel): Observable<any> {
    const formData = this.convertUserToFormData(user);

    return this.httpClient.post(`${this.baseUrl}/customers`, formData);
  }

  createAdmin(user: CreateUserModel): Observable<any> {
    const formData = this.convertUserToFormData(user);

    return this.httpClient.post(`${this.baseUrl}/admins`, formData);
  }

  createTechnicalSupportSpecialist(user: CreateUserModel): Observable<any> {
    const formData = this.convertUserToFormData(user);

    return this.httpClient.post(`${this.baseUrl}/technical-support-specialists`, formData);
  }

  private convertUserToFormData(user: CreateUserModel): FormData {
    var formData = new FormData();

    formData.append('image', user.image);
    formData.append('username', user.username);
    formData.append('fullName', user.fullName);
    formData.append('email', user.email);
    formData.append('dateOfBirth', `${user.dateOfBirth.year}-${user.dateOfBirth.month}-${user.dateOfBirth.day}`);
    formData.append('password', user.password);
    formData.append('confirmPassword', user.confirmPassword);

    return formData;
  }

  deleteUser(userId: string): Observable<any> {
    return this.httpClient.delete(`${this.baseUrl}/${userId}`);
  }

  confirmEmail(userId: string): Observable<any> {
    return this.httpClient.put(`${this.baseUrl}/${userId}/confirm-email`, null);
  }

  changePassword(model: ChangePasswordModel): Observable<any> {
    return this.httpClient.put(`${this.baseUrl}/passwords`, model);
  }

  deleteOwnAccount(): Observable<any> {
    return this.httpClient.delete(`${this.baseUrl}`);
  }

  blockUser(model: BlockUserModel): Observable<any> {
    return this.httpClient.put(`${this.baseUrl}/block`, model);
  }

  unblockUser(userId: string): Observable<any> {
    return this.httpClient.put(`${this.baseUrl}/${userId}/unblock`, null);
  }

  getStaffDataTableApiUrl() {
    return `${this.baseUrl}/staff/list`;
  }

  getStaffDataTableOptions() {
    var options = {
      title: 'Staff users',
      resourceName: 'user',
      showIndexColumn: true,
      allowCreating: true,
      createFormOptions: {
        form: new FormGroup({
          username: new FormControl(null, [Validators.required]),
          name: new FormControl(null, [Validators.required]),
          surname: new FormControl(null, [Validators.required]),
          email: new FormControl(null, [Validators.required]),
          dateOfBirth: new FormControl(null, [Validators.required]),
          role: new FormControl(null, [Validators.required]),
          password: new FormControl(null, [Validators.required]),
          confirmPassword: new FormControl(null, [Validators.required]),
        }),
        properties: [
          {
            label: 'Username',
            propName: 'username',
            type: FormInputTypeEnum.Text
          },
          {
            label: 'Name',
            propName: 'name',
            type: FormInputTypeEnum.Text
          },
          {
            label: 'Surname',
            propName: 'surname',
            type: FormInputTypeEnum.Text
          },
          {
            label: 'Email',
            propName: 'email',
            type: FormInputTypeEnum.Text
          },
          {
            label: 'Date of birth',
            propName: 'dateOfBirth',
            type: FormInputTypeEnum.Date
          },
          {
            label: 'Role',
            propName: 'role',
            type: FormInputTypeEnum.Select,
            selectOptions: [
              {
                label: 'Admin',
                value: UserRoleEnum[UserRoleEnum.Admin]
              },
              {
                label: 'Technical support specialist',
                value: UserRoleEnum[UserRoleEnum.TechnicalSupportSpecialist]
              }
            ]
          },
          {
            label: 'Password',
            propName: 'password',
            type: FormInputTypeEnum.Password
          },
          {
            label: 'Confirm password',
            propName: 'confirmPassword',
            type: FormInputTypeEnum.Password
          },
        ],
      },
      allowEdit: false,
      editFormOptions: null,
      allowDelete: true,
      optionalAction: null,
      emptyListDisplayLabel: 'The list of staff users is empty.',
      columnSettings: [
        {
          title: 'Username',
          dataPropName: 'username',
          isOrderable: true,
          width: 30
        },
        {
          title: 'Full name',
          dataPropName: 'fullName',
          isOrderable: true,
          width: 30
        },
        {
          title: 'Email',
          dataPropName: 'email',
          isOrderable: true,
          width: 30
        },
        {
          title: 'Date of birth',
          dataPropName: 'dateOfBirth',
          isOrderable: true,
          width: 30,
        },
        {
          title: 'Role',
          dataPropName: 'role',
          isOrderable: true,
          width: 30
        }
      ]
    } as DataTableOptionsModel;

    return options;
  }

  getCustomersDataTableApiUrl() {
    return `${this.baseUrl}/customers/list`;
  }

  getCustomersDataTableOptions(areBlocked: boolean = false) {
    var options = {
      title: null,
      resourceName: 'user',
      showIndexColumn: true,
      allowCreating: false,
      createFormOptions: null,
      allowEdit: false,
      editFormOptions: null,
      allowDelete: false,
      optionalAction: null,
      emptyListDisplayLabel: 'The list of customers is empty.',
      columnSettings: [
        {
          title: 'Username',
          dataPropName: 'username',
          isOrderable: true,
          width: 30
        },
        {
          title: 'Full name',
          dataPropName: 'fullName',
          isOrderable: true,
          width: 30
        },
        {
          title: 'Email',
          dataPropName: 'email',
          isOrderable: true,
          width: 30
        },
        {
          title: 'Date of birth',
          dataPropName: 'dateOfBirth',
          isOrderable: true,
          width: 30,
        },
        {
          title: 'Status',
          dataPropName: 'status',
          isOrderable: false,
          width: 30
        }
      ]
    } as DataTableOptionsModel;

    if (!areBlocked) {
      options.optionalAction = {
        actionName: 'Block',
        form: new FormGroup({
          id: new FormControl(null, [Validators.required]),
          blockingReason: new FormControl(null, [Validators.required]),
          days: new FormControl(null)
        }),
        properties: [
          {
            label: 'Blocking reason',
            propName: 'blockingReason',
            type: FormInputTypeEnum.TextArea
          },
          {
            label: 'Blocking days(optional)',
            propName: 'days',
            type: FormInputTypeEnum.Number,
          }
        ],
        message: null
      } as OptionalActionModel;
    } else {
      options.optionalAction = {
        actionName: 'Unblock',
        form: null,
        properties: null,
        message: 'Are you shure you want to unblock this user?'
      } as OptionalActionModel;
    }

    return options;
  }
}
