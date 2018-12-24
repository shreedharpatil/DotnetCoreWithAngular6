import { Component, OnInit } from '@angular/core';
import { AdalService } from 'adal-angular4';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
  token: any;
  isLoggedIn: boolean = false;
  constructor(private adalSvc: AdalService) { }

  ngOnInit() {
    this.token = this.adalSvc.userInfo.token;
    this.isLoggedIn = this.adalSvc.userInfo.authenticated; 
  }

  logout(): void {
    this.adalSvc.logOut();
  }
}
