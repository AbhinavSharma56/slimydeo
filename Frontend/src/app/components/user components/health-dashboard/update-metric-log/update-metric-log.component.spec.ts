import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateMetricLogComponent } from './update-metric-log.component';

describe('UpdateMetricLogComponent', () => {
  let component: UpdateMetricLogComponent;
  let fixture: ComponentFixture<UpdateMetricLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateMetricLogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateMetricLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
