import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { course } from '../models/cours';
import { Pagination } from '../models/pagination';
import { CourseParams } from '../models/params/CourseParams';
import { AccountService } from '../services/account.service';
import { CoursesService } from '../services/courses.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  Users: any;
  courses: course[] = [];
  coursrParams: CourseParams = new CourseParams;
  pagination: Pagination | undefined;


  constructor(private accountService: AccountService,
    private router: Router,
    private courseService: CoursesService) { }

  ngOnInit(): void {
    if (this.accountService.user)
      this.router.navigateByUrl('/courses');
    this.getCourses();
  }
  getCourses() {
    this.coursrParams.pageSize = 3;
    this.coursrParams.pageNumber = 1;
    this.courseService.getCourses(this.coursrParams).subscribe(
      res => {
        this.courses = res.result as course[];
        this.pagination = res.pagination;
      });
  }
  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode($event: boolean) {
    this.registerMode = $event;
  }

}
