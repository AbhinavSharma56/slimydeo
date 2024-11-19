import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteMetricLogComponent } from './delete-metric-log.component';

describe('DeleteMetricLogComponent', () => {
  let component: DeleteMetricLogComponent;
  let fixture: ComponentFixture<DeleteMetricLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteMetricLogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteMetricLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
