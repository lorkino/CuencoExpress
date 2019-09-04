import { EventEmitter, Injectable } from '@angular/core';  
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';  
import { Notification } from './Models/Notification';
import { User } from './Models/user';
import { ExpressService } from './express.service';
import { CommonService } from './services/common/common.service';

  
@Injectable()  
export class SignalRService {  
  messageReceived = new EventEmitter<string>();  
  connectionEstablished = new EventEmitter<Boolean>();  
  
  private connectionIsEstablished = false;  
  private _hubConnection: HubConnection;  
  public notificationsBadge: string[];
  
  constructor(private expressService: ExpressService, private commonService: CommonService) {  
    this.createConnection();  
    this.registerOnServerEvents();  
    this.startConnection();  
  }  
  
  sendChatMessage(message: string) {  
    this._hubConnection.invoke('SendMessage', message);  
  }  
  
  private createConnection() {  
    this._hubConnection = new HubConnectionBuilder()  
      .withUrl(window.location.origin+'/chathub')  
      .build();  
  }  
  
  private startConnection(): void {  
    this._hubConnection  
      .start()  
      .then(() => {  
        this.connectionIsEstablished = true;  
        console.log('Hub connection started');  
        this.connectionEstablished.emit(true);  
      })  
      .catch(err => {  
        console.log('Error while establishing connection, retrying...');  
        setTimeout((<any>this).startConnection(), 5000);  
      });  
  }  
  
  private registerOnServerEvents(): void {  
    this._hubConnection.on('ReceiveMessage', (data: any) => {
      console.log(data);
      this.messageReceived.emit(data);  
    });
    this._hubConnection.on('NotificationsNavMenu', (data: string) => {
       
      
      let obj : Notification[] = JSON.parse(data);
      this.expressService.user.notifications = obj;
      console.log(this.expressService.user);
      this.commonService.setSubject("UpdateBadgetFromSignalR");
      //this.messageReceived.emit(data);
    }); 
    this._hubConnection.onclose(async () => {
      await this.startConnection();
    });
  }  
} 
