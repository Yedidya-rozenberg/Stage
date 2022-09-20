import { AfterViewInit, Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { courseUpdate } from '../models/courseUpdate';
import { courseUnits } from '../models/courseUnits';
import { AccountService } from '../services/account.service';
import { CoursesService } from '../services/courses.service';
import { course } from '../models/cours';
import { NgForm, NgModel } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UnitsService } from '../services/units.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  course!: courseUnits;
  TeacherMode: boolean = false;
  editMode: boolean = false;
  updateForm!: courseUpdate;
  @ViewChild('Form') form!: NgForm;





  constructor(private route: ActivatedRoute,
    private coursesService: CoursesService,
    private unitsService: UnitsService,
    private accountService: AccountService,
    private toastr: ToastrService
  ) { }


  ngOnInit(): void {
    this.loadCourse();
    this.isTeacher();
  }
  loadCourse() {
    this.coursesService.courentCourse$.pipe(take(1)).subscribe(
      course => { this.course = course as courseUnits })
    this.updateForm = { courseStatus: this.course.details!.courseStatus, courseDescription: this.course.details!.courseDescription, courseName: this.course.details!.courseName };
  }
  isTeacher() {
    this.TeacherMode = this.coursesService.TeacherMode;
  }

  switchStatus() {
    this.updateForm.courseStatus = !this.course.details?.courseStatus;
    this.coursesService.updateCourse(this.course.details?.courseID as number, this.updateForm).subscribe(
      () => {
        this.toastr.success("Course status updated");
      }
    );
    this.loadCourse();
  }

  update() {
    this.coursesService.updateCourse(this.course.details?.courseID as number, this.updateForm).subscribe(
      () => {
        this.toastr.success("Course successfully updated");
      }
    );
    this.loadCourse();
    this.form.reset();
  }
  toggleEditMode() {
    this.editMode = !this.editMode;
    this.unitsService.editMode = this.editMode;
  }


}
