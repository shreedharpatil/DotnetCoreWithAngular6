import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';
import { environment } from "src/environments/environment";

@Component({
  selector: 'app-view-feeder',
  templateUrl: './view-feeder.component.html',
  styleUrls: ['./view-feeder.component.css']
})
export class ViewFeederComponent implements OnInit {
  villages: any = [];
  states: any = [];
  feeders: any = [];
  districtFeeders = [];
  talukFeeders = [];
  villageFeeders = [];
  activeTab = 'district';
  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.httpClient.get(environment.apiBase + 'State').subscribe(p => {
      this.states = p;
      this.getDistrictFeeders();
      this.getTalukFeeders();
      this.getvillageFeeders();
      this.feeders = this.districtFeeders;
    });
  }

  addFeeder(data) {
    this.httpClient.post(environment.apiBase + 'Feeder/' + this.activeTab + "/" + data.id, { Name : data.FeederName, Description : data.FeederDescription }).subscribe(p => {
      alert('Feeder saved successfully.');
      data.FeederName = null;
      data.FeederDescription = null;
      data.addFeeder = false;
      this.activeTab = 'district';  
      this.loadData();
    },
      error => { alert('An error occurred. Try again later.'); });
  }

  addTransformer(typeId, data) {
    this.httpClient.post(environment.apiBase + 'Transformer/' + this.activeTab + "/" + typeId + "/" + data.id, { Name: data.TransformerName, Description: data.TransformerDescription }).subscribe(p => {
      alert('Transformer saved successfully.');
      data.TransformerName = null;
      data.FeederDescrTransformerDescriptioniption = null;
      data.addTransformer = false;
        this.activeTab = 'district';
      this.loadData();
      },
      error => { alert('An error occurred. Try again later.'); });
  }

  getDistrictFeeders() {
    this.districtFeeders = [];
    for (let s of this.states) {
      for (let d of s.districts) {
        if (d.feeders != null && d.feeders.length > 0) {
          for (let f of d.feeders) {
            f.toggle = false;
          }
        }

        d.toggle = false;
        this.districtFeeders.push(d);
      }
    }
  }

  getTalukFeeders() {
    this.talukFeeders = [];
    for (let s of this.states) {
      for (let d of s.districts) {
        for (let t of d.taluks) {
          if (t.feeders != null && t.feeders.length > 0) {
            for (let f of t.feeders) {
              f.toggle = false;
            }
          }
          t.toggle = false;
          this.talukFeeders.push(t);
        }
      }
    }
  }

  getvillageFeeders() {
    this.villageFeeders = [];
    for (let s of this.states) {
      for (let d of s.districts) {
        for (let t of d.taluks) {
          for (let v of t.villages) {
            if (v.feeders != null && v.feeders.length > 0) {
              for (let f of v.feeders) {
                f.toggle = false;
              }
            }
            v.toggle = false;
            this.villageFeeders.push(v);
          }
        }
      }
    }
  }

  select(type) {
    this.activeTab = type;
    if (type == "district") {
      this.feeders = this.districtFeeders;
    }
    else if (type == "taluk") {
      this.feeders = this.talukFeeders;
    } else {
      this.feeders = this.villageFeeders;
    }
  }
}
