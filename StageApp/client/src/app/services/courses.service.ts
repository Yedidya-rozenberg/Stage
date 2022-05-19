import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { course } from '../models/cours';
import { User } from '../models/user';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {

  baseUrl = environment.apiUrl;
  courses:course[] = [];
  user: User | undefined;
  
  constructor(private http:HttpClient, private accountService: AccountService) { 
    accountService.currentUser$.pipe(take(1)).subscribe((user:any)=> {
      this.user = user;
    })
}
getCoueses(): void {

} 
getMyCourses():void{

}

addCourse():void{

}

getCourse():void{

}
}