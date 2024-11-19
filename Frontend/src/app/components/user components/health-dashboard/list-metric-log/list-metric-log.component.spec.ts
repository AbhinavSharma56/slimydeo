import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListMetricLogComponent } from './list-metric-log.component';

describe('ListMetricLogComponent', () => {
  let component: ListMetricLogComponent;
  let fixture: ComponentFixture<ListMetricLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListMetricLogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListMetricLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
