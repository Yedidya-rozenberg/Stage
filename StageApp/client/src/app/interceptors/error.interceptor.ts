import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private toaster: ToastrService
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        catchError(err => {
          switch (err.status) {
            case 400:
              if (err.error?.errors) {
                const modelStateErrors = [];
                for (const key in err.error.errors) {
                  if (err.error.errors[key]) {
                    modelStateErrors.push(err.error.errors[key]);
                  }
                }
                throw modelStateErrors.flat();
              } else if (typeof (err) === 'object') {
                this.toaster.error((err.statusText === 'OK' ? "Bed request</br>" + (err.error?err.error:"") : err.statusText), err.satus,{ closeButton: true, timeOut: 4000, progressBar: true, enableHtml: true } )
                throw err;
              }
              else {
                this.toaster.error(err.error, err.satus);
                throw err;
              }
              break;
            case 401:
              this.toaster.error((err.statusText === 'OK' ? "Unauthorizied</br>" + (err.error?err.error:"") : err.statusText) , err.satus,{ closeButton: true, timeOut: 4000, progressBar: true, enableHtml: true } )
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              this.toaster.error((err.statusText === 'OK' ? "Server error</br>" + (err.error?err.error:"") : err.statusText), err.satus,{ closeButton: true, timeOut: 4000, progressBar: true, enableHtml: true })
              break;
            default:
              this.toaster.error("Somthing unexpected want wrong");
              console.log(err);
              break;
          }
          throw throwError(() => err);
        })
      )
  }

}
