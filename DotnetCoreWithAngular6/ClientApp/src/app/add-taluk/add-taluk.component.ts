import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-taluk',
  templateUrl: './add-taluk.component.html',
  styleUrls: ['./add-taluk.component.css']
})
export class AddTalukComponent implements OnInit {
  states: any = [];
  taluk: any = { Name: '' };
  stateId: any;
  districts: any = [];
  districtId: any;

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
   this.httpClient.get('api/State').subscribe(p => { this.states = p; this.districts = this.states[0].districts; });
  }

  saveTaluk() {
    this.httpClient.post('api/Taluk/' + this.stateId + "/" + this.districtId, this.taluk).subscribe(p => { console.log(p); alert('saved successfully'); });
  }

  stateChanged() {
    var state = this.states.filter(p => p.id == this.stateId);
    if (state != null) {
      this.districts = state[0].districts;
      this.districtId = null;
    }
  }
}
