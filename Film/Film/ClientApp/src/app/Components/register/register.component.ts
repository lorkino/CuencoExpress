import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
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
  constructor(private fb: FormBuilder,
    private accountService: ExpressService,
    private router: Router) { }
  formGroup: FormGroup;
  ngOnInit() {
    this.formGroup = this.fb.group({
      email: '',
      password: '',
      password2: '',
    });
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
    if (error && error.error) {
      toastr.error(error.error[0].description, 'Error:');
    }
  }

  MatchPassword() {
    if (this.formGroup.value.password2 != this.formGroup.value.password && this.formGroup.value.password2 != "") {
      document.getElementById("noMatch").style.display = "block";
      var inputValue = <HTMLInputElement>document.getElementsByClassName("btn btn-primary")[0];
      inputValue.disabled = true;

    }
    else if (this.formGroup.value.password != "" && this.formGroup.value.password2 != "" && this.formGroup.value.email != ""){
      document.getElementById("noMatch").style.display = "none";
      var inputValue = <HTMLInputElement>document.getElementsByClassName("btn btn-primary")[0];
      inputValue.disabled = false;
    }
  }

}
