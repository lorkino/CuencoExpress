import { Component, HostListener } from '@angular/core';
import { PushsuscriberService } from './services/pushsuscriber.service';
import { SignalRService } from './signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  constructor(private notificationMiddleware: PushsuscriberService ) {
  }

  ngOnInit() {
    //REGISTRA el service worker
    this.notificationMiddleware.init();
    
  }


}
