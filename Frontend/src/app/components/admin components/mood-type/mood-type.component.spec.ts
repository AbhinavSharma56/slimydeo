import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoodTypeComponent } from './mood-type.component';

describe('MoodTypeComponent', () => {
  let component: MoodTypeComponent;
  let fixture: ComponentFixture<MoodTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MoodTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MoodTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
