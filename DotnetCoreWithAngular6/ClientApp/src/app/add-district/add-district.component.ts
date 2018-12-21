import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-district',
  templateUrl: './add-district.component.html',
  styleUrls: ['./add-district.component.css']
})
export class AddDistrictComponent implements OnInit {

  states: any = [];
  district: any = { Name: '' };
  stateId: any;
  

  ngOnInit() {
    this.httpClient.get('api/State').subscribe(p => { console.log(p); this.states = p; });
  }

  saveDistrict() {
    this.httpClient.post('api/District/' + this.stateId, this.district).subscribe(p => { console.log(p); alert('Saved successfully'); });
  }

}
