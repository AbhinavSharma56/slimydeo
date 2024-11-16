import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoodDashboardComponent } from './mood-dashboard.component';

describe('MoodDashboardComponent', () => {
  let component: MoodDashboardComponent;
  let fixture: ComponentFixture<MoodDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MoodDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MoodDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
