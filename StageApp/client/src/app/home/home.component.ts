import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../models/user';
// import { MembersService } from '../services/members.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

registerMode = false;
Users:any;

constructor(private http:HttpClient, 
  // private membersService:MembersService, 
  private router: Router ) { }

  ngOnInit(): void {
    // if (this.membersService.user)
    this.router.navigateByUrl('/members');
  }
  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode($event: boolean){
    this.registerMode = $event;
  }

}
