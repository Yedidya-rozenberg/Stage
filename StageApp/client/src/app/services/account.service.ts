import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject } from 'rxjs';
import { map, take } from "rxjs/operators";
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  
  private currentUserSource$ = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource$.asObservable();
  user: User | undefined;

  constructor(private http: HttpClient) {
   this.currentUser$.pipe(take(1)).subscribe((user:any)=> {
      this.user = user;
   })
  }
  
  
  login(model: any){
    return this.http.post<User>(this.baseUrl + 'account/login', model)
      .pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            this.setCurrentUser(user);
          }
        }));
  }


  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource$.next(user);
    this.user = user;
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource$.next(null);

  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model)
      .pipe(
        map((user: User) => {
          if(user){
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource$.next(user);
          }
          return user;
        })
      )
  }

  updateDetiles():void{
    
  }
  
}