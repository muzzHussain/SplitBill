import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupService } from 'src/app/Services/groupService';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css'],
  providers: [GroupService],
})
export class MembersComponent implements OnInit {
  alert: boolean = false;
  delUser: boolean = false;
  addMem: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  datal: any;
  id: any;
  name: string = ' ';
  groupId: string = '';
  members: any[] = [];
  AddmemberForm!: FormGroup;
  constructor(
    private _fb: FormBuilder,
    private route: ActivatedRoute,
    private GroupService: GroupService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.AddmemberForm = this._fb.group({
      EmailId: ['', [Validators.required]],
    });
    this.id = this.route.snapshot.params['id'];
    this.getIndvisualGroup();
  }
  getIndvisualGroup() {
    this.GroupService.getIndivisualGroup(this.id).subscribe((resp) => {
      this.datal = resp;

      this.members = this.datal[0].groupMembers;
      this.name = this.datal[0].groupName;
    });
  }

  deleteUserFromGroup(userId: string) {
    this.groupId = this.datal[0].id;
    this.GroupService.deleteUserFromGroup(this.groupId, userId).subscribe(
      (resp) => {
        this.delUser = true;
        this.alert = true;
        this.alertClass = 'alert alert-success';
        this.successMessage = 'User Deleted successfully';
        setTimeout(() => {
          this.alert = false;
          this.getIndvisualGroup();
        }, 800);
      },
      (err) => {
        this.delUser = true;
        console.log('error', err);
        this.alert = true;
        this.alertClass = 'alert alert-danger';
        this.errorMessage = 'Something bad happened';
        setTimeout(() => {
          this.alert = false;
        }, 800);
      }
    );
  }

  addMember() {
    console.log('Member email', this.AddmemberForm.get('EmailId')?.value);

    const userEmail = this.AddmemberForm.get('EmailId')?.value;
    this.groupId = this.id;

    this.GroupService.addUser(this.groupId, userEmail).subscribe(
      (resp) => {
        alert('Member Added Successfully');
        setTimeout(() => {
          this.getIndvisualGroup();
          // this.router.navigate([`/group/members/${this.id}`]);
        }, 800);
      },
      (err) => {
        console.log('error', err);
        alert('Something went wrong, Try Later.');
      }
    );
  }

  goToAddMembers() {
    this.groupId = this.datal[0].id;
    this.router.navigate([`group/add/members/${this.groupId}`]);
  }

  closeAlert() {
    this.alert = false;
  }
}
