import { TestBed } from '@angular/core/testing';

import { HealthMetricService } from './health-metric.service';

describe('HealthMetricService', () => {
  let service: HealthMetricService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthMetricService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
