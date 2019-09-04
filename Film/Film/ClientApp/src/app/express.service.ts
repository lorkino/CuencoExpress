import { Injectable, Injector } from '@angular/core';
import { Observable, of, Observer } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from "./Models/user";
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserDates } from './Models/userDates';
import { CommonService } from './services/common/common.service';
import { SignalRService } from './signal-r.service';
@Injectable({
  providedIn: 'root',
})
export class ExpressService {
  public user: User;
  
  constructor(private http: HttpClient, private router: Router,
    private commonService: CommonService) {
  }

  login(email: string, password: string, rememberme: boolean): Observable<any> {
    return Observable.create((observer: Observer<any>) => {
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
            let date = new Date();
            localStorage.setItem('tokenDate', date.toISOString());
            this.commonService.setSubject("activandoBugdetDeNavMenu");         
            observer.next(response)
            observer.complete()
            this.router.navigate(['/']);
          });
    })

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
    this.delete_cookie(".AspNetCore.Identity.Application");
  }
  //borra todo el almacenamiento del server si las versiones localstorage de cliente son mas antiguas
  checkClientStorageVersion(dateServer) {
    if (dateServer > localStorage.getItem("tokenDate")) {
      localStorage.clear();

    }
  }

  delete_cookie(name) {
  document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
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
