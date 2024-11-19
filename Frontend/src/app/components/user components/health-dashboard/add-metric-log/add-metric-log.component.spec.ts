import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMetricLogComponent } from './add-metric-log.component';

describe('AddMetricLogComponent', () => {
  let component: AddMetricLogComponent;
  let fixture: ComponentFixture<AddMetricLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddMetricLogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddMetricLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
