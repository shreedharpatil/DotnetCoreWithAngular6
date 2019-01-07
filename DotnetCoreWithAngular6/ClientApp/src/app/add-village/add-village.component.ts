import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";

@Component({
  selector: 'app-add-village',
  templateUrl: './add-village.component.html',
  styleUrls: ['./add-village.component.css']
})
export class AddVillageComponent implements OnInit {
  states: any = [];
  village: any = { Name: '' };
  stateId: any = 0;
  districts: any = [];
  taluks: any = [];
  talukId: any = 0;
  districtId: any = 0;
  addFeeder: any = false;
  feeder: any = {};

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.httpClient.get(environment.apiBase + 'State').subscribe(p => {
    this.states = p;
      //this.districts = this.states[0].districts; this.taluks = this.districts[0].taluks;
    });
  }

  saveVillage() {
    if (this.addFeeder && this.feeder.Name != null) {
      this.village.Feeders = [this.feeder];
    }
    this.httpClient.post(environment.apiBase + 'Village/' + this.stateId + "/" + this.districtId + "/" + this.talukId, this.village).subscribe(p => { console.log(p); alert('saved successfully'); });
  }

  stateChanged() {
    var state = this.states.filter(p => p.id == this.stateId);
    this.districtId = 0;
    this.talukId = 0;
    this.districts = [];
    this.taluks = [];
    if (state != null) {
      this.districts = state[0].districts;      
    }
  }

  districtChanged() {
    var district = this.districts.filter(p => p.id == this.districtId);
    this.talukId = 0;
    this.taluks = [];
    if (district != null) {
      this.taluks = district[0].taluks;
    }
  }
}
