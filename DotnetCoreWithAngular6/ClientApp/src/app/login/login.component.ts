import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AdalService } from 'adal-angular4';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  title = 'Advanced Energy Meter App';
  credentials = { Username: 'shreedhar', Password: 'patil' };

  constructor(private httpClient: HttpClient, private router: Router, private route: ActivatedRoute,
    private adalSvc: AdalService) { }

  ngOnInit() {
  }

  login(): void {
    const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    if (this.adalSvc.userInfo.authenticated) {
      this.router.navigate([returnUrl]);
    } else {
      this.adalSvc.login();
    }
  }

  validateuserCredentials() {
    this.httpClient.post('api/Login', this.credentials).subscribe(p => { console.log(p); if (p) { this.router.navigate(['/viewusers']); } else { alert('invalid credentials'); } });
  }
}
