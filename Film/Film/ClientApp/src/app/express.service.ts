import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from "./Models/user";
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class ExpressService {
  
  constructor(private http: HttpClient) {

  }

  getUser(email: string, password: string, rememberme: boolean): Observable<User> {
    
    return this.http.post<User>('api/login',
      {
        Email: email,
        Password: password,
        RememberMe: rememberme
      }
    )
  }

  create(userInfo: User): Observable<any> {
    return this.http.post<any>( "api/register", userInfo);
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

  getProfile(form: FormGroup): Observable<any> {
    return this.http.get<any>("api/profile");
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
    if (exp=="undefined" || token=="null") {
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
