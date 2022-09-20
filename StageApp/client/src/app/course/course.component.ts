import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { courseUpdate } from '../models/courseUpdate';
import { courseUnits } from '../models/courseUnits';
import { AccountService } from '../services/account.service';
import { CoursesService } from '../services/courses.service';
import { course } from '../models/cours';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  course!: courseUnits;
  TeacherMode: boolean = false;
  editMode: boolean = false;

  constructor(private route: ActivatedRoute,
    private coursesService: CoursesService,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.loadCourse();
    this.isTeacher();
  }
  loadCourse() {
    this.coursesService.courentCourse$.pipe(take(1)).subscribe(
      course => { this.course = course as courseUnits })
  }
  isTeacher() {
    this.accountService.currentUser$.pipe(take(1)).subscribe(
      user => { this.TeacherMode = (user?.username == this.course?.details?.teacherName) })
  }

  switchStatus() {
    const params: courseUpdate = { courseStatus: !this.course.details?.courseStatus, courseDescription: this.course.details?.courseDescription as string, courseName: this.course.details?.courseName as string };
    this.coursesService.updateCourse(this.course.details?.courseID as number, params).subscribe();
    this.loadCourse();
  }


}
