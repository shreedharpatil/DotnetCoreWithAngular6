import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'ClientApp';
  credentials = { Username: 'username', Password: 'pwd' };

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.httpClient.get('api/sampledata/WeatherForecasts').subscribe(p => console.log(p));
  }

  validateuserCredentials() {
    this.httpClient.post('api/Login', this.credentials).subscribe(p => { console.log(p); alert(p); });
  }
}
