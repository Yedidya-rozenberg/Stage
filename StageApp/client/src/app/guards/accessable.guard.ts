import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { CoursesService } from '../services/courses.service';

@Injectable({
  providedIn: 'root'
})
export class AccessableGuard implements CanActivate {

  constructor(
    private coursesService: CoursesService,
    private toaster: ToastrService
  ) { }
  canActivate(): Observable<boolean> {
    return this.coursesService.courentCourse$.pipe(
      map(course => {
        if (course) return true;
        this.toaster.error("You can't access this course");
        return false;
      })
    )
  }
}
