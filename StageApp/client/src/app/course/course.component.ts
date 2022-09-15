import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { course } from '../models/cours';
import { courseUnits } from '../models/courseUnits';
import { CoursesService } from '../services/courses.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  course!: courseUnits;

  constructor(private route: ActivatedRoute,
    private coursesService: CoursesService) { }

  ngOnInit(): void {
    this.loadCourse();
  }
  loadCourse() {
    // const routeParams = this.route.snapshot.paramMap;
    // const courseName = routeParams.get('courseName') as string;
    // this.coursesService.getCourse(courseName).subscribe((course:courseUnits) => {
    //   this.course = course;
    this.coursesService.courentCourse$.pipe(take(1)).subscribe(
      course => { this.course = course as courseUnits })
  }
}
