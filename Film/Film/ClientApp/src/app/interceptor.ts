import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpSentEvent, HttpHeaderResponse, HttpProgressEvent, HttpUserEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ExpressService } from './express.service';
import { throwError } from 'rxjs';
import { map, catchError, finalize } from 'rxjs/operators';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { LoadingScreenService } from './services/loading-screen/loading-screen.service';
declare var toastr: any;
@Injectable({
  providedIn: 'root',
})
export class MyInterceptor implements HttpInterceptor {
  activeRequests: number = 0;
  constructor(private expressService: ExpressService, private loadingScreenService: LoadingScreenService) {


  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {

    if (this.activeRequests === 0) {
      this.loadingScreenService.startLoading();
    }

    this.activeRequests++;
    //rutas excluidas 
    let excludeRoutes: Array<string> = [
                                        'https://api.ipdata.co/?api-key=test',
                                        'https://ms-autocomplete.spain.schibsted.io/skills/'
                                       ];

    //Mensajes de toast
    enum Messages {
      Profile = "Su perfil se ha actualizado correctamente",
      Job = "Se ha publicado el trabajo correctamente",     
      Left = "LEFT",
      Right = "RIGHT",
    }
    
    var token = this.expressService.obtenerToken();
     
    //Comprobar si hacemos peticion a nuestra API o a otra del exterior
    if (excludeRoutes.indexOf(req.url) == -1 && req.url.search(excludeRoutes[1]) ==-1)
      req = req.clone({
        setHeaders: { Authorization: "bearer " + token }
      });
    else {
      this.loadingScreenService.stopLoading();
      return next.handle(req);
    }

    const that = this;
    //HANDLE error
    return next.handle(req).pipe(
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          console.log('event--->>>', event);
          if ((<any>Object).values(Messages).includes(event.body)) {
            that.expressService.updateLocalUser();
            toastr.success(event.body);
          }
          else {
            if (event.body!=null)
            if(event.body.value!=null)
            toastr.success(event.body.value);
          }

          // this.errorDialogService.openDialog(event);
        }
        return event;
      }),
      finalize(() => {
        that.activeRequests--;
        if (that.activeRequests === 0) {
          that.loadingScreenService.stopLoading();
        }
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
