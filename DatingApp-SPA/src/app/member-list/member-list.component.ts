import { Component, OnInit } from '@angular/core';
import { User } from '../_models/User';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];

  constructor(
    private userservice: UserService,
    private alertfy: AlertifyService
  ) {}

  ngOnInit() {
    this.loadUsers();
  }
  loadUsers() {
    this.userservice.getUsers().subscribe(
      (users: User[]) => {
        this.users = users;
      },
      error => {
        this.alertfy.error(error);
      }
    );
  }
}
