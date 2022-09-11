import { Component, Input, OnInit } from '@angular/core';
import { course } from '../models/cours';

@Component({
  selector: 'app-course-card',
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.css']
})
export class CourseCardComponent implements OnInit {

  constructor() { }
  @Input() course! :course;

  ngOnInit(): void {
  }

}
