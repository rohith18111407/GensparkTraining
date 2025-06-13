import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Weatherdashboard } from './weatherdashboard';

describe('Weatherdashboard', () => {
  let component: Weatherdashboard;
  let fixture: ComponentFixture<Weatherdashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Weatherdashboard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Weatherdashboard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
