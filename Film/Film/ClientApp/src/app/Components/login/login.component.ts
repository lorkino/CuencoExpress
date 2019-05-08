import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ExpressService } from '../.././express.service';
import { User } from '../.././Models/user';
import { Router } from "@angular/router";
import { AuthService, FacebookLoginProvider, GoogleLoginProvider } from 'angular5-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  profileForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    checkPassword: new FormControl(false)
  });
 
  constructor(private expressService: ExpressService, private router: Router, private socialAuthService: AuthService ) {

  }

  ngOnInit() {
    
  }
  onSubmit() { 
    this.expressService.login(this.profileForm.value.email, this.profileForm.value.password, this.profileForm.value.checkPassword != true ? false : this.profileForm.value.checkPassword);  
  }

  public facebookLogin() {
    let socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    this.socialAuthService.signIn(socialPlatformProvider).then(
      (userData) => {
        
        //this will return user data from facebook. What you need is a user token which you will send it to the server
       // this.sendToRestApiMethod(userData.token);
        this.router.navigate(['/login']);
        console.log(userData);
        let token: any = userData.token;
       
        localStorage.setItem('token', token);
        var date:Date = new Date();
        date.setDate(date.getDate() + 1);
        var tokenExpiration: any = date.toLocaleString();
        localStorage.setItem('tokenExpiration', tokenExpiration);
        this.router.navigate(['/']);
        console.log(userData);
      }
    );
    
    


  }
}
