import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

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

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('',[Validators.required,Validators.maxLength(8),Validators.minLength(4)]),
      confirmPassword: new FormControl('',[Validators.required,Validators.maxLength(8),Validators.minLength(4)]),
    }, this.passwordMatchValidater);
  }
  passwordMatchValidater(f:FormGroup) {
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
