import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {};
  constructor(private authServices: AuthService) { }

  ngOnInit() {
  }
logIn() {
 this.authServices.logIn(this.model).subscribe(next => {
   console.log('Logged In Successfully');
 }, error => {
   console.log('Failed to Login');
 }
  ) ;
}
loggedIn(){
  const token = localStorage.getItem('token');
  return !!token;
}
logOut(){
  localStorage.removeItem('token');
  console.log('Logged Out')
}
}
