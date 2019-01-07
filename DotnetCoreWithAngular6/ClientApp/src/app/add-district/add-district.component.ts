import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-district',
  templateUrl: './add-district.component.html',
  styleUrls: ['./add-district.component.css']
})
export class AddDistrictComponent implements OnInit {

  states: any = [];
  district: any = { Name: '' };
  feeder: any = {};
  stateId: any = 0;
  addFeeder: any = false;
  constructor(private httpClient: HttpClient) {
  }

  ngOnInit() {
    this.httpClient.get(environment.apiBase + 'State').subscribe(p => { console.log(p); this.states = p; });
  }

  saveDistrict() {
    if (this.addFeeder && this.feeder.Name != null) {
      this.district.Feeders = [this.feeder];
    }
    this.httpClient.post(environment.apiBase + 'District/' + this.stateId, this.district).subscribe(p => { console.log(p); alert('Saved successfully'); });
  }

}
