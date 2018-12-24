import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-loadshedding-info',
  templateUrl: './loadshedding-info.component.html',
  styleUrls: ['./loadshedding-info.component.css']
})
export class LoadsheddingInfoComponent implements OnInit {
  feederId: any = 0;
  message: any = null;
  states: any = [];
  districts: any = [];
  taluks: any = [];
  villages: any = [];
  feeders: any = [];
  transformers: any = [];
  stateId: any = 0;
  talukId: any = 0;
  districtId: any = 0;
  villageId: any = 0;

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.httpClient.get('api/State').subscribe(p => {
      this.states = p;
    });
  }

  sendMessage() {
    this.httpClient.post('api/SendMessage', { FeederId: this.feederId, Message: this.message })
      .subscribe(p => {
        alert('Message sent successfully.');
        console.log(p);
        this.message = null;
        this.stateId = 0;
        this.districtId = 0;
        this.talukId = 0;
        this.villageId = 0;
        this.feederId = 0;
      }
        , error => alert('An error occurred. Try again later.'));
  }

  stateChanged() {
    var state = this.states.filter(p => p.id == this.stateId)[0];
    this.taluks = [];
    this.districtId = 0;
    this.talukId = 0;
    this.feederId = 0;
    this.districts = [];
    if (state != null) {
      this.districts = state.districts;
    }
  }

  districtChanged() {
    var district = this.districts.filter(p => p.id == this.districtId)[0];
    this.taluks = [];
    this.feeders = [];
    this.villages = [];
    this.talukId = 0;
    this.villageId = 0;
    this.feederId = 0;
    if (district != null) {
      this.taluks = district.taluks;
      this.feeders = district.feeders;
    }
  }

  talukChanged() {
    var taluk = this.taluks.filter(p => p.id == this.talukId)[0];
    this.villages = [];
    this.feeders = [];
    this.villageId = 0;
    this.feederId = 0;
    this.transformers = [];
    if (taluk != null) {
      this.villages = taluk.villages;
      this.feeders = taluk.feeders;
    }
  }

  villageChanged() {
    var village = this.villages.filter(p => p.id == this.villageId)[0];
    this.feederId = 0;
    this.transformers = [];
    if (village != null) {
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
}
