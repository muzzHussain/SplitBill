import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GroupService } from '../Services/groupService';
import GroupRequestDTO from '../models/GroupDTO';
import { UserService } from '../Services/userService';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  alert: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  groupDetails: GroupRequestDTO[] = [];
  userName: string = '';

  constructor(
    private _httpCline: HttpClient,
    private _groupService: GroupService,
    private _userService: UserService
  ) {}

  async ngOnInit(): Promise<void> {
    this.userName = await this._userService.getUserName();

    setInterval(() => {
      this.fetchAllGroups();
    }, 500);
    // this._groupService.fetchAllGroup().subscribe({
    //   next: (resp) => {
    //     console.log('groups:', resp);
    //     this.groupDetails = resp;
    //   },
    //   error: (err) => {
    //     console.error('Error', err);
    //   },
    // });
  }

  fetchAllGroups() {
    this._groupService.fetchAllGroup().subscribe({
      next: (resp) => {
        this.groupDetails = resp;
      },
      error: (err) => {
        console.error('Error', err);
      },
    });
  }

  onDelete(id: string) {
    this._groupService.deleteGroup(id).subscribe(
      (resp) => {
        if (resp) {
          this.alert = true;
          this.alertClass = 'alert alert-success';
          this.successMessage = 'Deleted Successfully';
          setTimeout(() => {
            this.alert = false;
            this.fetchAllGroups();
          }, 1500);
        }
      },
      (err) => {
        console.error('Error', err);
        this.alert = true;
        this.alertClass = 'alert alert-danger';
        this.successMessage = 'Something happened';
      }
    );
  }

  onLeaveGroup(id: string) {
    this._groupService.leaveGroup(id).subscribe(
      (resp) => {
        if (resp) {
          this.alert = true;
          this.alertClass = 'alert alert-success';
          this.successMessage = 'You have left the group successfully';
          setTimeout(() => {
            this.alert = false;
          }, 1000);
        }
      },
      (err) => {
        this.alert = true;
        this.alertClass = 'alert alert-danger';
        if (err.error && err.error.includes("Can't leave the group")) {
          this.errorMessage =
            "Can't leave the group; at least one member should be in the group";
        }
        setTimeout(() => {
          this.alert = false;
        }, 2000);
      }
    );
  }

  closeAlert() {
    this.alert = false;
  }
}
