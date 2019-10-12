import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(
    public authServices: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {}


  nullOrEmpty(value: string) {
    return value === undefined || value === '';
  }

  login() {

    const { username, password } = this.model;


    if (this.nullOrEmpty(username) || this.nullOrEmpty(password)) {
      this.alertify.error('UserName or Password is Required');

      return;
    }

    this.authServices.login(this.model).subscribe(
      next => {
        this.alertify.success('Logged In Successfully');
      },
      error => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['/member']);
      }
    );
  }

  loggedIn() {
    return this.authServices.loggedIn();
  }

  logOut() {
    localStorage.removeItem('token');
    this.alertify.message('Logged Out');
    this.router.navigate(['/home']);
  }
}
