import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpSentEvent, HttpHeaderResponse, HttpProgressEvent, HttpUserEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ExpressService } from './express.service';

@Injectable({
  providedIn: 'root',
})
export class MyInterceptor implements HttpInterceptor {
  constructor(private expressService: ExpressService ) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
    var token = this.expressService.obtenerToken();
    req = req.clone({
      setHeaders: { Authorization: "bearer " + token }
    });
    return next.handle(req);
  }
}
