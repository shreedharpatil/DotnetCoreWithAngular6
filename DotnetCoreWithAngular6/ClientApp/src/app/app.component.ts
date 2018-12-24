import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AdalService } from 'adal-angular4';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  isLoggedIn: boolean = false;
  constructor(private adalSvc: AdalService, private router: Router) {
    this.adalSvc.init(environment.azureConfig);
  }

  ngOnInit(): void {
    this.adalSvc.handleWindowCallback();
    this.isLoggedIn = this.adalSvc.userInfo.authenticated;    
  }
}
