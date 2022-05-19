import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any ={};
//loggedIn: boolean = false;
currentUser$ : Observable<User | null>;

  constructor(private accountService: AccountService,
    private router: Router,
    private toaster:ToastrService) {
     this.currentUser$ = this.accountService.currentUser$;
   }

  ngOnInit(): void {
    //console.log(this.model);
    //this.getCurrnetUser();
  }
  login(){
    this.accountService.login(this.model)
    .subscribe({next: Response=> {
      this.router.navigateByUrl('/members');
      console.log(Response);
     // this.loggedIn = true;
    },
    error: (error)=> {console.log('faild to login', error)
  this.toaster.error(error.error)},
  complete: ()=>{console.log('Login complete')}});
      //.subscribe(Response => {console.log(Response);
      // this.loggedin = true;}
  }
  // getCurrnetUser() {
  //   this.accountService.currentUser$.subscribe((user:User | null) => {
  //     this.loggedIn = !!user;
  //   });
  // }

  logout(){
    //this.loggedIn = false;
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
