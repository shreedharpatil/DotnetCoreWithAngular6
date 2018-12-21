import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  user: User = new User();
  constructor(private httpClient:HttpClient, private router:Router) { }

  ngOnInit() {
  }

  createUser() {
    this.httpClient.post('api/User', this.user).subscribe(p => { console.log(p); this.router.navigate(['viewusers']); });
  }
}

class User {
  RRNo: string;
  Firstname: string;
  Lastname: string;
  Email: string;
  Mobile: string;
  Address: string;
  EMMobile: string;
}
