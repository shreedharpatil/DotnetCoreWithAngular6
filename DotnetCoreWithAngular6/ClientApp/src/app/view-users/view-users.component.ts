import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent implements OnInit {
  users: any = [];
  cardView = true;
  constructor(private httpClient: HttpClient) {
  }

  ngOnInit() {
    this.httpClient.get(environment.apiBase + 'User').subscribe(p => { console.log(p); this.users = p; });
  }
}
