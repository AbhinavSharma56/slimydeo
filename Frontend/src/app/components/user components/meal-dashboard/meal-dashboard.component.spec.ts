import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealDashboardComponent } from './meal-dashboard.component';

describe('MealDashboardComponent', () => {
  let component: MealDashboardComponent;
  let fixture: ComponentFixture<MealDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MealDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MealDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
