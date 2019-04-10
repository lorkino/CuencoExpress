import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpSentEvent, HttpHeaderResponse, HttpProgressEvent, HttpUserEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ExpressService } from './express.service';
import { throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
declare var toastr: any;
@Injectable({
  providedIn: 'root',
})
export class MyInterceptor implements HttpInterceptor {
  constructor(private expressService: ExpressService ) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
    var token = this.expressService.obtenerToken();
    console.log(req);
    if (req.url.search(location.origin)!=-1)
    req = req.clone({
      setHeaders: { Authorization: "bearer " + token }
    });

    //HANDLE error
    return next.handle(req).pipe(
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          //console.log('event--->>>', event);
          // this.errorDialogService.openDialog(event);
        }
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        let data = {};
        data = {
          reason: error.error,
          status: error.status
        };
        //this.errorDialogService.openDialog(data);
        console.log(data);
        toastr.error(data["reason"], 'Error:');
        return throwError(error);
      }));

  }
}
