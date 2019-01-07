import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {environment} from "src/environments/environment";

@Component({
  selector: 'app-add-state',
  templateUrl: './add-state.component.html',
  styleUrls: ['./add-state.component.css']
})
export class AddStateComponent implements OnInit {
  state: any = { Name: '' };
  constructor(private httpClient: HttpClient) {
  }

  ngOnInit() {
  }

  saveState() {
    this.httpClient.post(environment.apiBase + 'State', this.state).subscribe(p => { console.log(p); alert('Saved successfully'); });
  }
}
