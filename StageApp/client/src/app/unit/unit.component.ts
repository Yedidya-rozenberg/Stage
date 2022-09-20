import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { course } from '../models/cours';
import { courseUnits } from '../models/courseUnits';
import { unit } from '../models/unit';
import { CoursesService } from '../services/courses.service';
import { UnitsService } from '../services/units.service';

@Component({
  selector: 'app-unit',
  templateUrl: './unit.component.html',
  styleUrls: ['./unit.component.css']
})
export class UnitComponent implements OnInit {
  unit!: unit;
  course!: courseUnits;
  editMode: boolean = false;
  questionsList: string[] = [];
  TeacherMode: boolean = false;



  constructor(private route: ActivatedRoute,
    private unitService: UnitsService,
    private coursesService: CoursesService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loudCourse();
    this.loudUnit();
  }


  loudCourse() {
    this.coursesService.courentCourse$.subscribe(course => this.course = course as courseUnits);
  }

  loudUnit() {
    this.route.data.subscribe(data => {
      this.unit = data['unit'];
      this.questionsList = this.unit.questions.split(".");
    });
    this.isTeacher();
    this.editMode = this.unitService.editMode;
  }

  EditToggle() {
    this.editMode = !this.editMode;
    this.unitService.editMode = this.editMode;
  }

  isTeacher() {
    this.TeacherMode = this.coursesService.TeacherMode;
  }

  update() {
    this.unitService.updateUnit(this.unit).subscribe(
      (unit:unit) => {
        this.unit = unit;
        this.toastr.success("Unit updated");
        this.questionsList = unit.questions.split(".");
      }
    );
    this.EditToggle();
  }

}
