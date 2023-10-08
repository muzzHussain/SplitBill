import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormControlName,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { Router } from '@angular/router';
import { GroupService } from '../Services/groupService';
import { CreateGroupDTO, Users } from '../models/CreateGroupDTO';
import { emailListValidator } from 'src/CustomeValidations&Pipe/email-list.validator';
import { emitDistinctChangesOnlyDefaultValue } from '@angular/compiler';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css'],
})
export class CreateGroupComponent implements OnInit {
  alert = false;
  createGroupForm!: FormGroup;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  userLogin: boolean = false;
  dynamicUsersArray!: FormArray;
  showDynamicUsers: boolean = false;
  addingUser: boolean = false;

  constructor(
    private _fb: FormBuilder,
    private _route: Router,
    private _groupService: GroupService
  ) {}

  ngOnInit(): void {
    this.createGroupForm = this._fb.group({
      title: ['', [Validators.required]],
      UserList: ['', [Validators.required]],
      dynamicUsers: this._fb.array([]),
    });
    this.addUserControl();
  }

  get getTitle() {
    return this.createGroupForm.get('title');
  }

  get getUserList() {
    return this.createGroupForm.get('userList');
  }

  get dynamicUsers(): FormArray {
    return this.createGroupForm.get('dynamicUsers') as FormArray;
  }

  getDynamicUserControl(index: number): FormControl {
    return this.dynamicUsers.at(index) as FormControl;
  }

  addUserControl() {
    const userControl = this._fb.control('');
    this.dynamicUsers.push(userControl);
  }

  private createUser(): FormGroup {
    return this._fb.group({
      emailId: ['', [Validators.required, Validators.email]],
    });
  }

  get userListFormArray() {
    return this.createGroupForm.get('UserList');
  }

  addUser() {
    this.showDynamicUsers = true;
    this.addUserControl();

    // const newUserControl = this._fb.control('', [Validators.required]);
    // this.dynamicUsers.push(newUserControl);
  }

  removeUser(index: number) {
    this.dynamicUsers.removeAt(index);
  }

  createGroup() {
    console.log('form value', this.createGroupForm.value);

    if (this.createGroupForm.get('title')?.valid) {
      const dynamicUsersList = this.getEmailsArrayFromList(
        this.createGroupForm.value.dynamicUsers
      );

      const formData: CreateGroupDTO = {
        title: this.createGroupForm.value.title,
        UsersList: [
          ...this.getEmailsArray(this.createGroupForm.value.UserList),
          ...dynamicUsersList.filter((email) => email.EmailId !== ''),
        ],
      };

      this._groupService.createGroup(formData).subscribe(
        (resp) => {
          this.alertClass = 'alert alert-success';
          this.alert = true;
          this.successMessage = resp;
          setTimeout(() => {
            this._route.navigateByUrl('/dashboard');
          }, 1000);
        },
        (error) => {
          this.alertClass = 'alert alert-danger';
          this.alert = true;
          this.errorMessage = error;
        }
      );
    } else {
      this.validateAllFields(this.createGroupForm);
    }
  }

  private getEmailsArray(emailList: string | null): Users[] {
    if (!emailList) {
      return [];
    } else {
      return [{ EmailId: emailList.trim() }];
    }
  }

  private getEmailsArrayFromList(emailList: string[] | null): Users[] {
    if (!emailList || emailList.length === 0) {
      return [];
    } else {
      return emailList.map((email) => ({ EmailId: email.trim() }));
    }
  }

  private validateAllFields(formGroup: FormGroup | FormArray) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) this.validateAllFields(control);
    });
  }

  closeAlert() {
    this.alert = false;
  }
}
