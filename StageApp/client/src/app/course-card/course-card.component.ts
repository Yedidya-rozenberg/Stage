import { Component, Input, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { course } from '../models/cours';
import { CoursesService } from '../services/courses.service';

@Component({
  selector: 'app-course-card',
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.css']
})
export class CourseCardComponent implements OnInit {

  constructor(
    private courseService: CoursesService,
    private router: Router
  ) { }
  @Input() course! :course;

  ngOnInit(): void {
  }
  loadCourse(){
    this.courseService.getCourse(this.course.courseName);
    this.router.navigateByUrl("/course");
  }

}
