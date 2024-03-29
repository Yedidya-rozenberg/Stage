import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { courseUpdate } from '../models/courseUpdate';
import { courseUnits } from '../models/courseUnits';
import { CoursesService } from '../services/courses.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UnitsService } from '../services/units.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit, OnDestroy {
  course!: courseUnits;
  TeacherMode: boolean = false;
  editMode: boolean = false;
  removeMode: boolean = false;
  view: boolean = true;
  updateForm!: courseUpdate;
  @ViewChild('Form') form!: NgForm;
  _subscription!: Subscription





  constructor(private coursesService: CoursesService,
    private unitsService: UnitsService,
    private toastr: ToastrService,
  ) { }


  ngOnInit(): void {
    this.loadCourse();
    this.isTeacher();
  }
  loadCourse() {
    this._subscription = this.coursesService.courentCourse$.pipe().subscribe(
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
    this.removeMode = this.editMode ? false : this.removeMode;
  }
  toggleRemoveMode() {
    this.removeMode = !this.removeMode;
    this.editMode = this.removeMode ? false : this.editMode;
    this.unitsService.editMode = this.editMode;
  }

  removeUnit(unitID: number) {
    this.view = false;
    this.unitsService.removeUnit(unitID)
      .subscribe(() => {
        this.course.units = this.course.units?.filter(unit => unit.unitID !== unitID);
        this.toastr.success("Unit successfully removed");
      })
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }
}
