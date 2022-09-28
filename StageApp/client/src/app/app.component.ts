import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Stage';
  constructor(private http: HttpClient,
    private accountService: AccountService,
    private router: Router) { }

  ngOnInit(): void {
    this.setCurrentUser();
    this.router.navigateByUrl('/')
  }

  setCurrentUser() {
    const userFromLS: any = localStorage.getItem('user');
    const user = JSON.parse(userFromLS);
    if (user) {
      this.accountService.setCurrentUser(user);
    }
  }
}
