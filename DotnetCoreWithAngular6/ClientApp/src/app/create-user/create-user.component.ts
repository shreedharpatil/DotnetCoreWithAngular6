import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  user: any = {};
  states: any = [];
  districts: any = [];
  taluks: any = [];
  villages: any = [];
  feeders: any = [];
  stateId: any;
  talukId: any;
  districtId: any;
  villageId: any;

  constructor(private httpClient:HttpClient, private router:Router) { }

  ngOnInit() {
    this.httpClient.get('api/State').subscribe(p => {
      this.states = p;
      this.districts = this.states[0].districts;
      this.taluks = this.districts[0].taluks;
      this.villages = this.taluks[0].villages;
    });
  }

  createUser() {
    this.httpClient.post('api/User', this.user).subscribe(p => { console.log(p); this.router.navigate(['viewusers']); });
  }

  stateChanged() {
    var state = this.states.filter(p => p.id == this.stateId)[0];
    if (state != null) {
      this.user.State = state.name;
      this.user.District = null;
      this.user.Taluk = null;
      this.user.Village = null;
      this.districts = state.districts;
      this.taluks = [];
      this.districtId = null;
      this.talukId = null;
    }
  }

  districtChanged() {
    var district = this.districts.filter(p => p.id == this.districtId)[0];
    if (district != null) {
      this.user.District = district.name;
      this.user.Taluk = null;
      this.user.Village = null;
      this.taluks = district.taluks;
      this.feeders = district.feeders;
      this.villages = [];
      this.talukId = null;
      this.villageId = null;
    }
  }

  talukChanged() {
    var taluk = this.taluks.filter(p => p.id == this.talukId)[0];
    if (taluk != null) {
      this.user.Taluk = taluk.name;
      this.user.Village = null;
      this.villageId = null;
      this.villages = taluk.villages;
      this.feeders = taluk.feeders;
    }
  }

  villageChanged() {
    var village = this.villages.filter(p => p.id == this.villageId)[0];
    if (village != null) {
      this.user.Village = village.name;
      if (village.feeders != null && village.feeders.length > 0) {
        this.feeders = village.feeders;
      }
      else {
        var taluk = this.taluks.filter(p => p.id == this.talukId)[0];
        if (taluk != null) {
          this.feeders = taluk.feeders;
        }
      }
    }
  }
}
