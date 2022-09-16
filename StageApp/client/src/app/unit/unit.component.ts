import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  course!:courseUnits;

  constructor(private route: ActivatedRoute,
    private unitService: UnitsService,
    private courseService:CoursesService) { }

  ngOnInit(): void {
    this.loudCourse();
    this.loudUnit();
  }


  loudCourse() {
this.courseService.courentCourse$.subscribe(course=>this.course=course as courseUnits);
  }

  loudUnit() {
    const routeParams = this.route.snapshot.paramMap;
    const unitId = routeParams.get('unitId') as string;
    this.unitService.getUnit(unitId).subscribe(unit => {
      this.unit = unit;
    });
  }
  }
