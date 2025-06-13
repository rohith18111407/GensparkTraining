import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Weathercard } from './weathercard';

describe('Weathercard', () => {
  let component: Weathercard;
  let fixture: ComponentFixture<Weathercard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Weathercard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Weathercard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
