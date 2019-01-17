import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup } from '@angular/forms';
import { ExpressService } from '../express.service';
import { User } from '.././Models/user';
import { Router } from "@angular/router"
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  profileForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
    checkPassword: new FormControl('')
  });
  user: User;
 
  constructor(private expressService: ExpressService, private router: Router) {
    this.user = new User();

  }

  ngOnInit() {
    
  }
  onSubmit() {
    var that = this.user;
    this.expressService.getUser(this.profileForm.value.email, this.profileForm.value.password, this.profileForm.value.checkPassword != true ? false : this.profileForm.value.checkPassword ).subscribe(
      response => {
        this.user = response;

        localStorage.setItem('token', response.token);
        localStorage.setItem('tokenExpiration', response.tokenExpiration);
        this.router.navigate(['/']);
      }
    );
    
    console.log(this.user);
  }
}
