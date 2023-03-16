import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { course } from '../models/cours';
import { courseUpdate } from '../models/courseUpdate';
import { PaginatedResult } from '../models/pagination';
import { CourseParams } from '../models/params/CourseParams';
import { User } from '../models/user';
import { AccountService } from './account.service';
import { BehaviorSubject, Observable, of, take, tap } from 'rxjs';
import { getPaginatedResult, getPaginationParams } from './paginationHelper';
import { courseUnits } from '../models/courseUnits';
import { unitName } from '../models/unitName';
import { unitParams } from '../models/params/unitParams';
import { UnitsService } from './units.service';


@Injectable({
  providedIn: 'root'
})
export class CoursesService {

  baseUrl = environment.apiUrl;
  courses: course[] = [];
  user: User | undefined;
  courseCache = new Map<string, PaginatedResult<course[]>>();
  myCourseCache = new Map<string, PaginatedResult<course[]>>();
  courseParams: CourseParams = new CourseParams;
  unitParams: unitParams = new unitParams;
  private courentCourseSource$ = new BehaviorSubject<courseUnits | null>(null);
  courentCourse$ = this.courentCourseSource$.asObservable();
  TeacherMode: boolean = false;




  constructor(private http: HttpClient,
    private accountService: AccountService,
    private unitsService: UnitsService) {
    accountService.currentUser$.pipe(take(1)).subscribe((user: any) => {
      this.user = user;
    })
  }
  getCourses(courseParams: CourseParams): Observable<PaginatedResult<course[]>> {
    const cacheKay = Object.values(courseParams).join('-');
    const response = this.courseCache.get(cacheKay);
    if (response) { return of(response); }

    let params = getPaginationParams(courseParams.pageNumber, courseParams.pageSize);
    params = params.append('MyCourses', courseParams.MyCourses);
    if (courseParams.TeacherName !== undefined) {
      params = params.append('TeacherName', courseParams.TeacherName);
    }

    return getPaginatedResult<course[]>(this.baseUrl + 'courses', params, this.http)
      .pipe(tap(response => {
        this.courseCache.set(cacheKay, response);
        if (courseParams.MyCourses === true) {
          this.myCourseCache.set(cacheKay, response);
        }
      }));
  }

  getCourse(courseName: string) {
    let fullCourse: courseUnits = new courseUnits;
    const courses = [...this.myCourseCache.values()];
    const allcourses = courses.reduce((arr: course[], elem: PaginatedResult<course[]>) => arr.concat(elem.result as course[]), []);
    const foundCourse = allcourses.find(c => c.courseName === courseName);

    if (foundCourse) fullCourse.details = foundCourse as course;
    else {
      this.http.get<course>(this.baseUrl + 'courses/' + courseName).subscribe(
        {
          next: (course: course) => {
            fullCourse.details = course;
          },
          error: (error) => {
            this.courentCourseSource$.next(null);
          }
        }
      )
    }
    this.unitParams.courseId = fullCourse.details?.courseID as number;
    this.unitsService.getUnits(this.unitParams).subscribe(
      {
        next: (units: PaginatedResult<unitName[]>) => {
          fullCourse.units = units.result;
        }
      });
    this.courentCourseSource$.next(fullCourse);
    this.isTeacher();
  }

  updateCourse(id: number, params: courseUpdate): Observable<course> {
    const fullCourse: courseUnits = this.courentCourseSource$.value as courseUnits;
    return this.http.put<course>(this.baseUrl + 'courses/' + id, params).pipe
      (tap((course) => {
        this.myCourseCache.clear();
        const photoUrl = fullCourse.details!.photoUrl;
        fullCourse.details = course;
        fullCourse.details!.photoUrl = photoUrl;
        this.courentCourseSource$.next(fullCourse);
      }));
  }
  isTeacher() {
    const fullCourse: courseUnits = this.courentCourseSource$.value as courseUnits;
    this.accountService.currentUser$.pipe(take(1)).subscribe(
      user => { this.TeacherMode = (user?.username == fullCourse.details?.teacherName) });
  }

  addCourse(): void {

  }

}
