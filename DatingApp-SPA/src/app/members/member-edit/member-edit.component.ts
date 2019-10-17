import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/User';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
 editForm: NgForm;
  user: User;

  constructor(private router: ActivatedRoute, private alertify: AlertifyService) {}

  ngOnInit() {
    this.router.data.subscribe(data => {
      this.user = data.user;
    });
  }
  updateUser() {
    console.log(this.user);
    this.alertify.success('Data saved');
    this.editForm.reset(this.user);

  }
}
