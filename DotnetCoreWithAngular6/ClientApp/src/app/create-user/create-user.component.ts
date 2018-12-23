import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  feederId: any = 0;
  user: any = { Feeder : 0, Transformer : 0 };
  states: any = [];
  districts: any = [];
  taluks: any = [];
  villages: any = [];
  feeders: any = [];
  transformers : any = [];
  stateId: any = 0;
  talukId: any = 0;
  districtId: any = 0;
  villageId: any = 0;

  constructor(private httpClient:HttpClient, private router:Router) { }

  ngOnInit() {
    this.httpClient.get('api/State').subscribe(p => {
      this.states = p;
      //this.districts = this.states[0].districts;
      //this.taluks = this.districts[0].taluks;
      //this.villages = this.taluks[0].villages;
    });
  }

  createUser() {
    this.httpClient.post('api/User', this.user).subscribe(p => { console.log(p); this.router.navigate(['viewusers']); });
  }

  stateChanged() {
    var state = this.states.filter(p => p.id == this.stateId)[0];
    this.user.District = null;
    this.user.Taluk = null;
    this.user.Village = null;
    this.user.Feeder = 0;
    this.taluks = [];
    this.districtId = 0;
    this.talukId = 0;
    this.feederId = 0;
    this.user.State = null;
    this.districts = [];
    if (state != null) {
      this.user.State = state.name;
      this.districts = state.districts;
    }
  }

  districtChanged() {
    var district = this.districts.filter(p => p.id == this.districtId)[0];
    this.user.District = null;
    this.taluks = [];
    this.feeders = [];
    this.user.Taluk = null;
    this.user.Village = null;
    this.user.Feeder = 0;
    this.villages = [];
    this.talukId = 0;
    this.villageId = 0;
    this.feederId = 0;
    this.user.Transformer = 0;
    if (district != null) {
      this.user.District = district.name;
      this.taluks = district.taluks;
      this.feeders = district.feeders;
    }
  }

  talukChanged() {
    var taluk = this.taluks.filter(p => p.id == this.talukId)[0];
    this.user.Taluk = null;
    this.villages = [];
    this.feeders = [];
    this.user.Village = null;
    this.user.Feeder = 0;
    this.villageId = 0;
    this.feederId = 0;
    this.transformers = [];
    this.user.Transformer = 0;
    if (taluk != null) {
      this.user.Taluk = taluk.name;
      this.villages = taluk.villages;
      this.feeders = taluk.feeders;
    }
  }

  villageChanged() {
    var village = this.villages.filter(p => p.id == this.villageId)[0];
    this.user.Feeder = 0;
    this.feederId = 0;
    this.user.Transformer = 0;
    this.transformers = [];
    this.user.Village = null;
    if (village != null) {
      this.user.Village = village.name;
      if (village.feeders != null && village.feeders.length > 0) {
        this.feeders = village.feeders;
      } else {
        let taluk = this.taluks.filter(p => p.id == this.talukId)[0];
        if (taluk != null) {
          this.feeders = taluk.feeders;
        }
      }
    } else {
      let taluk = this.taluks.filter(p => p.id == this.talukId)[0];
      if (taluk != null) {
        this.feeders = taluk.feeders;
      }
    }
  }

  feederChanged() {
    this.user.Feeder = this.feederId;
    this.user.Transformer = 0;
    var feeder = this.feeders.filter(p => p.id == this.feederId)[0];
    if (feeder != null) {
      this.transformers = [];
      if (feeder.transformers != null && feeder.transformers.length > 0) {
        this.transformers = feeder.transformers;
      }
    }
  }
}
