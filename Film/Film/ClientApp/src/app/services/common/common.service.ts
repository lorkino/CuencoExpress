import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor() { }

  public UserSubject: BehaviorSubject<any> = new BehaviorSubject(null);
  setSubject(value) {
    if (value) {
      this.UserSubject.next(value);
    } else {
      this.UserSubject.next(null)
    }
  }

}
