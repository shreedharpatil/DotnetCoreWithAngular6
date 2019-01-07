import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";

@Component({
  selector: 'app-add-taluk',
  templateUrl: './add-taluk.component.html',
  styleUrls: ['./add-taluk.component.css']
})
export class AddTalukComponent implements OnInit {
  states: any = [];
  taluk: any = { Name: '' };
  stateId: any = 0;
  districts: any = [];
  districtId: any = 0;
  addFeeder: any = false;
  feeder: any = {};

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.httpClient.get(environment.apiBase + 'State').subscribe(p => {
    this.states = p; //this.districts = this.states[0].districts;
    });
  }

  saveTaluk() {
    if (this.addFeeder && this.feeder.Name != null) {
      this.taluk.Feeders = [this.feeder];
    }
    this.httpClient.post(environment.apiBase + 'Taluk/' + this.stateId + "/" + this.districtId, this.taluk).subscribe(p => { console.log(p); alert('saved successfully'); });
  }

  stateChanged() {
    var state = this.states.filter(p => p.id == this.stateId);
    this.districtId = 0;
    this.districts = [];
    if (state != null) {
      this.districts = state[0].districts;
    }
  }
}
