import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { keyframes } from '@angular/animations';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private toaster: ToastrService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
    .pipe(
      catchError(err => {
        switch(err.status){
          case 400:
            if(err.error?.errors) {
              const modelStateErrors = [];
              for (const key in err.error.errors){
                if (err.error.errors[key]){
                  modelStateErrors.push(err.error.errors[key]);
                }
              }
              throw modelStateErrors.flat();
              } else if(typeof(err)==='object') {
                this.toaster.error((err.statusText=== 'OK'? "Bed request": err.statusText) , err.satus)
                throw err;
              }
              else {
                this.toaster.error(err.error, err.satus);
                throw err;
              }
          break;
          case 401:
            this.toaster.error(err.statusText=== 'OK'? "Unauthorizied": err.statusText, err.satus)
          break;          
          case 404:
            this.router.navigateByUrl('/not-found');
          break;          
          case 500:
             const navigationExtras:  NavigationExtras = {state:{error:err.error}}
             this.router.navigateByUrl('/server-error', navigationExtras);
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
