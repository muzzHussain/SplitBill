import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupService } from 'src/app/Services/groupService';

@Component({
  selector: 'app-addmembers',

  templateUrl: './addmembers.component.html',

  styleUrls: ['./addmembers.component.css'],

  providers: [GroupService],
})
export class AddmembersComponent {
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  alert = false;
  datal: any;
  id: any;
  AddmemberForm!: FormGroup;
  name: string = ' ';
  groupId: string = '';
  members: any[] = [];
  constructor(
    private route: ActivatedRoute,
    private GroupService: GroupService,
    private router: Router,
    private _fb: FormBuilder
  ) {}
  ngOnInit(): void {
    this.AddmemberForm = this._fb.group({
      EmailId: ['', [Validators.required]],
    });
    this.id = this.route.snapshot.params['id'];
  }

  closeAlert() {
    this.alert = false;
  }
  goToAddMembers() {
    this.groupId = this.datal[0].id;
    this.router.navigate([`group/add/members/${this.groupId}`]);
  }

  addMember() {
    const userEmail = this.AddmemberForm.get('EmailId')?.value;
    this.groupId = this.id;

    this.GroupService.addUser(this.groupId, userEmail).subscribe(
      (resp) => {
        this.alert = true;
        this.alertClass = 'alert alert-success';
        this.successMessage = 'User added successfully';
        setTimeout(() => {
          this.alert = false;
          this.router.navigate([`/group/members/${this.id}`]);
        }, 800);
      },
      (err) => {
        console.log('error', err);
      }
    );
  }
}
