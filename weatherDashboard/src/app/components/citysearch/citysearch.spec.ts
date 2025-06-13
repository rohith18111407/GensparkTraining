import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Citysearch } from './citysearch';

describe('Citysearch', () => {
  let component: Citysearch;
  let fixture: ComponentFixture<Citysearch>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Citysearch]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Citysearch);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
