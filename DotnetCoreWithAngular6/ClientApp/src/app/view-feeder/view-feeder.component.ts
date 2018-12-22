import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-feeder',
  templateUrl: './view-feeder.component.html',
  styleUrls: ['./view-feeder.component.css']
})
export class ViewFeederComponent implements OnInit {
  villages: any = [];
  states: any = [];
  feeders: any = [];
  activeTab = 'district';
  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.httpClient.get('api/State').subscribe(p => {
      this.states = p;
      this.select('district');
    });
  }

  addFeeder(data) {
    this.httpClient.post('api/Feeder/' + this.activeTab, { Id : data.id, Name : data.FeederName, Description : data.FeederDescription }).subscribe(p => {
      alert('Feeder saved successfully.');
      data.FeederName = null;
      data.FeederDescription = null;
      data.addFeeder = false;
      this.loadData();
    },
      error => { alert('An error occurred. Try again later.'); });
  }

  select(type) {
    this.activeTab = type;
    if (type == "district") {
      this.feeders = [];
      for (let s of this.states) {
        for (let d of s.districts) {
          this.feeders.push(d);
        }
      }
    }
    else if (type == "taluk") {
      this.feeders = [];
      for (let s of this.states) {
        for (let d of s.districts) {
          for (let t of d.taluks) {
            this.feeders.push(t);
          }
        }
      }
    } else {
      for (let s of this.states) {
        this.feeders = [];
        for (let d of s.districts) {
          for (let t of d.taluks) {
            for(let v of t.villages)
              this.feeders.push(v);
          }
        }
      }
    }
  }
}
