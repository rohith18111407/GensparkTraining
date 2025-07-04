import { ComponentFixture, TestBed } from '@angular/core/testing';
import { App } from './app';
import { PaymentComponent } from './payment/payment';

describe('App', () => {
  let fixture: ComponentFixture<App>;
  let component: App;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App, PaymentComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(App);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should render PaymentComponent', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('app-payment')).toBeTruthy();
  });

  it('should have correct title', () => {
    expect(component['title']).toBe('RazorPayUpiApp');
  });
});
