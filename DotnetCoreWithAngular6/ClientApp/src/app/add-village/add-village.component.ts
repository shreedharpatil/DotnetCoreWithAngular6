import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-village',
  templateUrl: './add-village.component.html',
  styleUrls: ['./add-village.component.css']
})
export class AddVillageComponent implements OnInit {
  states: any = [];
  village: any = { Name: '' };
  stateId: any;
  districts: any = [];
  taluks: any = [];
  talukId: any;
  districtId: any;
  addFeeder: any = false;
  feeder: any = {};

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
   this.httpClient.get('api/State').subscribe(p => { this.states = p; this.districts = this.states[0].districts; this.taluks = this.districts[0].taluks; });
  }

  saveVillage() {
    if (this.addFeeder && this.feeder.Name != null) {
      this.village.Feeders = [this.feeder];
    }
    this.httpClient.post('api/Village/' + this.stateId + "/" + this.districtId + "/" + this.talukId, this.village).subscribe(p => { console.log(p); alert('saved successfully'); });
  }

  stateChanged() {
    var state = this.states.filter(p => p.id == this.stateId);
    if (state != null) {
      this.districts = state[0].districts;
      this.taluks = [];
      this.districtId = null;
      this.talukId = null;
    }
  }

  districtChanged() {
    var district = this.districts.filter(p => p.id == this.districtId);
    if (district != null) {
      this.taluks = district[0].taluks;
      this.talukId = null;
    }
  }
}