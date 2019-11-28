import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import {BsDatepickerConfig} from 'ngx-bootstrap'
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output()
  cancelRegister = new EventEmitter();
  model: any = {};
  registerForm: FormGroup;
  bsConfig: BsDatepickerConfig; 

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private frombilder: FormBuilder
  ) {}

  ngOnInit() {
    this.createRegisterForm();
  }
 createRegisterForm(){
   this.registerForm = this.frombilder.group({
    gender: ['male'],
    username: ['',Validators.required],
    password: ['', [Validators.required, Validators.maxLength(8), Validators.minLength(4)]],
    confirmPassword: ['',[Validators.required, Validators.maxLength(8), Validators.minLength(4)]],
    knownAs: ['', Validators.required],
    city: ['', Validators.required],
    country: ['', Validators.required],
    dateOfBirth: [null, Validators.required],
   },{validator: this.passwordMatchValidater});
 }

  passwordMatchValidater(f: FormGroup) {
    return f.get('password').value === f.get('confirmPassword').value ? null : {'missmatch': true}
  }
  register() {
    //this.authService.register(this.model).subscribe(
      //() => {
        //this.alertify.success('Registration Susscesful');
      //},
     // error => {
      //  this.alertify.error(error);
     // }
    //);
    console.log(this.registerForm.value);
  }
  cencel() {
    this.cancelRegister.emit(false);
    this.alertify.warning('cancelled');
  }
}
