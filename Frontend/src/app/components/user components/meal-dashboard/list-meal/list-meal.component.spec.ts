import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListMealComponent } from './list-meal.component';

describe('ListMealComponent', () => {
  let component: ListMealComponent;
  let fixture: ComponentFixture<ListMealComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListMealComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListMealComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
