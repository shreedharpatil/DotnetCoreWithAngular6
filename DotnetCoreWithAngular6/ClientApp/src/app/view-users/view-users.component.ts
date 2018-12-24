import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdalService } from 'adal-angular4';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent implements OnInit {
  users: any = [];
  cardView = true;
  constructor(private httpClient: HttpClient, private adalService: AdalService) {
  }

  ngOnInit() {
    console.log(this.adalService.userInfo.token);
    console.log(this.adalService.userInfo);
    this.httpClient.get('api/User').subscribe(p => { console.log(p); this.users = p; });
  }
}
