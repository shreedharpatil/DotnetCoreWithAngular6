import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  user: User = new User();
  constructor(private httpClient:HttpClient) { }

  ngOnInit() {
  }

  createUser() {
    this.httpClient.post('api/User', this.user).subscribe(p => console.log(p));
  }
}

class User {
  RRNo: string;
  Firstname: string;
  Lastname: string;
  Email: string;
  Mobile: string;
  Address: string;
}
