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
  photoUrl: string;
  constructor(
    public authServices: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authServices.CurrentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl )
  }


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
    localStorage.removeItem('user');
    this.authServices.currentUser = null;
    this.authServices.decodedToken = null;
    this.alertify.message('Logged Out');
    this.router.navigate(['/home']);
  }
}
