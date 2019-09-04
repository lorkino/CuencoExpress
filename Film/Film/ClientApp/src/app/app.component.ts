import { Component, HostListener } from '@angular/core';
import { PushsuscriberService } from './services/pushsuscriber.service';
import { SignalRService } from './signal-r.service';
import { AppConfigProviderService, AppConfig } from './services/app-config-provider.service';
import { ExpressService } from './express.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  appConfig: AppConfig;
  constructor(private notificationMiddleware: PushsuscriberService, private appConfigProvider: AppConfigProviderService, private expressService: ExpressService) {
    this.appConfig = this.appConfigProvider.getConfig();
  }

  ngOnInit() {
    //REGISTRA el service worker
    this.notificationMiddleware.init();
    this.expressService.checkClientStorageVersion(this.appConfig.tokenDate);
    this.expressService.updateLocalUser();

  }


}
