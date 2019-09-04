import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


export declare type AppConfig = { [key: string]: string };
@Injectable({
  providedIn: 'root'
})
export class AppConfigProviderService {

  private config: AppConfig;

  constructor(
    private httpClient: HttpClient
  ) { }

  loadConfig() {
    return this.httpClient.get<AppConfig>('api/config').toPromise().then(x => {
      this.config = x;
      
    });
  }

  getConfig() {
    return this.config;
  }
}
