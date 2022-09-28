import { Component, OnInit } from '@angular/core';
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
  model: any = {};
  currentUser$: Observable<User | null>;

  constructor(private accountService: AccountService,
    private router: Router,
    private toaster: ToastrService) {
    this.currentUser$ = this.accountService.currentUser$;
  }

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model)
      .subscribe({
        next: Response => {
          this.router.navigateByUrl('/courses');
          console.log(Response);
        },
        error: (error) => {
          console.log('faild to login', error)
          this.toaster.error(error.error)
        },
        complete: () => { console.log('Login complete') }
      });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
