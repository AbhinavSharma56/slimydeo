import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetricTypeComponent } from './metric-type.component';

describe('MetricTypeComponent', () => {
  let component: MetricTypeComponent;
  let fixture: ComponentFixture<MetricTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MetricTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MetricTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
