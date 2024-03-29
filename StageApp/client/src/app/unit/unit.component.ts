import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  newUnit: boolean = false;
  questionsList: string[] = [];
  TeacherMode: boolean = false;



  constructor(private route: ActivatedRoute,
    private unitService: UnitsService,
    private coursesService: CoursesService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loudCourse();
    this.loudUnit();
  }


  loudCourse() {
    this.coursesService.courentCourse$.subscribe(course => this.course = course as courseUnits);
  }

  loudUnit() {
    this.isTeacher();
    this.editMode = this.unitService.editMode;
    this.route.data.subscribe(data => {
      this.unit = data['unit'];
      if (!this.unit.courseID) {
        if (!this.TeacherMode) {
          this.toastr.error("Unit not found");
          this.router.navigate(['/not-found']);
        }
        else {
          this.newUnit = true;
          this.editMode = true;
          this.unit.courseID = this.course.details!.courseID;
        }
      }
      this.questionsList = this.unit.questions? this.unit.questions.split("."):[];
    });

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
      (unit: unit) => {
        this.unit = unit;
        this.toastr.success("Unit updated");
        this.questionsList = unit.questions?unit.questions.split("."):[];
        this.coursesService.getCourse(this.course.details!.courseName);
      }
    );
    this.EditToggle();
  }
  addUnit() {
    this.unitService.addUnit(this.unit).subscribe(
      (unit: unit) => {
        this.toastr.success("Unit added");
        this.coursesService.getCourse(this.course.details!.courseName);
        this.router.navigate(['/course']);
      });
  }
}
