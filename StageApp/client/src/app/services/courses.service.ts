import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { course } from '../models/cours';
import { PaginatedResult } from '../models/pagination';
import { CourseParams } from '../models/params/CourseParams';
import { User } from '../models/user';
import { AccountService } from './account.service';
import { map, Observable, of, take, tap } from 'rxjs';
import { getPaginatedResult, getPaginationParams } from './paginationHelper';


@Injectable({
  providedIn: 'root'
})
export class CoursesService {

  baseUrl = environment.apiUrl;
  courses:course[] = [];
  user: User | undefined;
  courseCache = new Map<string, PaginatedResult<course[]>>();
  courseParams: CourseParams = new CourseParams;

  
  constructor(private http:HttpClient, private accountService: AccountService) { 
    accountService.currentUser$.pipe(take(1)).subscribe((user:any)=> {
      this.user = user;
    })
}
getCourses(courseParams:CourseParams): Observable<PaginatedResult<course[]>>{
  const cacheKay = Object.values(courseParams).join('-');
const response = this.courseCache.get(cacheKay);
if (response){return of (response);}

let params = getPaginationParams(courseParams.pageNumber, courseParams.pageSize);
params = params.append('MyCourses', courseParams.MyCourses);
if (courseParams.TeacherName !== undefined){
  params = params.append('TeacherName', courseParams.TeacherName);
}

return getPaginatedResult<course[]>(this.baseUrl + 'courses', params, this.http)
.pipe(tap(response => {this.courseCache.set(cacheKay, response);}));
} 
getMyCourses():void{

}

addCourse():void{

}

getCourse():void{

}
}