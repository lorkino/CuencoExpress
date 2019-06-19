import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from "./Models/user";
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserDates } from './Models/userDates';
@Injectable({
  providedIn: 'root',
})
export class ExpressService {
  public user: User;
  constructor(private http: HttpClient, private router: Router) {

  }

  login(email: string, password: string, rememberme: boolean) {
    this.http.post<User>('api/login',
      {
        Email: email,
        Password: password,
        RememberMe: rememberme
      }).subscribe(
      response => {
        //this.user = {
        //  accessFailedCount : response.accessFailedCount,
        //  admin: response.admin,
        //  email: response.email,
        //  emailConfirmed: response.emailConfirmed,
        //  rememberMe: response.rememberMe,
        //  token: response.token,
        //  tokenExpiration: response.tokenExpiration
        
        //};

        this.setUser(response);
        this.setUserStorage();
        localStorage.setItem('token', response.token);
        localStorage.setItem('tokenExpiration', response.tokenExpiration);
        this.router.navigate(['/']);
      });

  }

  setUser(user:User) {
    this.user = user;
  }
  setUserStorage() {
    localStorage.setItem('user', JSON.stringify(this.user));
  }
  getUser(): User {
    if (this.user == null)
      this.getUserFromStorage();
    return this.user;
  };
  updateLocalUser() {
    this.http.get<any>("api/profile/getMe").subscribe(
      response =>{ this.setUser(response) });
    this.setUserStorage();
  }
  getUserFromStorage() {
    this.setUser(JSON.parse(localStorage.getItem('user')));
  }
  create(userInfo: User): Observable<any> {
    return this.http.post<any>("api/register", userInfo);
  }

  changeProfile(userProfile: any): Observable<any> {
    return this.http.post<any>("api/profile", userProfile);
  }
  setKnowledges(knowledges: any): Observable<any> {
    return this.http.post<any>("api/profile/saveKnowledges", knowledges);
  }


  setKnowledgesJob(knowledges: any): Observable<any> {
    return this.http.post<any>("api/job/saveKnowledges", knowledges);
  }

  setJob(job: any): Observable<any> {
    return this.http.post<any>("api/job/createJob", job);
  }

  getProfile(): Observable<any> {
    return this.http.get<any>("api/profile"); 
  }

  getJobsSize():Observable<any> {
    return this.http.get<any>("api/job/jobsnumber");
  }

  getJobs():Observable<any> {
    return this.http.get<any>("api/job/jobs");
  }

  getOffersSize(): Observable<any> {
    return this.http.get<any>("api/job/offersnumber");
  }

  getOffers( i:number=1): Observable<any> {
    return this.http.get<any>("api/job/offers/"+i);
  }
  obtenerToken(): string {
    return localStorage.getItem("token");
  }

  obtenerExpiracionToken(): string {
    return localStorage.getItem("tokenExpiration");
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("tokenExpiration");
  }

  estaLogueado(): boolean {

    var exp = this.obtenerExpiracionToken();
    var token = this.obtenerToken();
    if (exp == "undefined" || token == "null") {
      // el token no existe
      return false;
    }

    var now = new Date().getTime();
    var dateExp = new Date(exp);

    if (now >= dateExp.getTime()) {
      // ya expiró el token
      localStorage.removeItem('token');
      localStorage.removeItem('tokenExpiration');
      return false;
    } else {
      return true;
    }

  }


}
