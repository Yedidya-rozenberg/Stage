import { Component, OnInit } from '@angular/core';
import { TruthyTypesOf } from 'rxjs';
import { course } from '../models/cours';
import { Pagination } from '../models/pagination';
import { CourseParams } from '../models/params/CourseParams';
import { CoursesService } from '../services/courses.service';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  courses: course[] = [];
  pagination: Pagination | undefined;
  storage = true;
  courseParams: CourseParams = {} as CourseParams;
  myCourses = true;

  constructor(private coursesService: CoursesService) {
    this.courseParams = this.coursesService.courseParams;
  }

  ngOnInit(): void {
    this.loadCourses()
  }
  loadCourses() {
    this.courseParams.MyCourses = this.myCourses;
    this.coursesService.getCourses(this.courseParams as CourseParams).subscribe(courses => {
      this.courses = courses.result as course[];
      this.pagination = courses.pagination;
    })
  }
  pageChanged(event: any) {
    this.courseParams.pageNumber = event.page;
    this.loadCourses();
  }

}
