import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewFeederComponent } from './view-feeder.component';

describe('ViewFeederComponent', () => {
  let component: ViewFeederComponent;
  let fixture: ComponentFixture<ViewFeederComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewFeederComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewFeederComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
