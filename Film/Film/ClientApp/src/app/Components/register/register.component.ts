import { Component, OnInit } from '@angular/core';
import { FormControl,FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../../Models/user';
import { ExpressService } from '../.././express.service';
import { Router } from '@angular/router';
declare var toastr: any;
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

 formGroup = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      password2: new FormControl('', [Validators.required])
    });
  passwordsAreEqual: boolean;
  constructor(private fb: FormBuilder,
    private accountService: ExpressService,
    private router: Router) { }
 
  ngOnInit() {
   
  }

   equalPassword() {
     let password = this.formGroup.controls['password'].value;
     let repeatPassword = this.formGroup.controls['password2'].value;
     this.passwordsAreEqual = ((password == repeatPassword) && password.length > 0 && repeatPassword.length > 0);
  }

  loguearse() {
    let userInfo: User = Object.assign({}, this.formGroup.value);
    this.accountService.create(userInfo).subscribe(token => this.recibirToken(token),
      error => this.manejarError(error));
  }

  registrarse() {
    let userInfo: User = Object.assign({}, this.formGroup.value);
    this.accountService.create(userInfo).subscribe(token => this.recibirToken(token),
      error => this.manejarError(error));
  }

  recibirToken(token) {
    localStorage.setItem('token', token.token);
    localStorage.setItem('tokenExpiration', token.expiration);
    this.router.navigate([""]);
  }

  manejarError(error) {
   
  }



}
