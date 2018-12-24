import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadsheddingInfoComponent } from './loadshedding-info.component';

describe('LoadsheddingInfoComponent', () => {
  let component: LoadsheddingInfoComponent;
  let fixture: ComponentFixture<LoadsheddingInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoadsheddingInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoadsheddingInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
