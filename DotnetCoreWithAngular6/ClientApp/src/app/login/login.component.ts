import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  title = 'ClientApp';
  credentials = { Username: 'shreedhar', Password: 'patil' };

  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit() {
    this.httpClient.get('api/sampledata/WeatherForecasts').subscribe(p => console.log(p));
  }

  validateuserCredentials() {
    this.httpClient.post('api/Login', this.credentials).subscribe(p => { console.log(p); if (p) { this.router.navigate(['/homepage']); } else { alert('invalid credentials'); } });
  }
}
