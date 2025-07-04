import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PaymentComponent } from './payment';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RazorpayService } from '../services/razorpay.service';
import { By } from '@angular/platform-browser';

describe('PaymentComponent', () => {
  let component: PaymentComponent;
  let fixture: ComponentFixture<PaymentComponent>;
  let mockRazorpayService: jasmine.SpyObj<RazorpayService>;

  beforeEach(async () => {
    mockRazorpayService = jasmine.createSpyObj('RazorpayService', ['createRazorpayOptions', 'launchRazorpay']);

    await TestBed.configureTestingModule({
      imports: [PaymentComponent, CommonModule, ReactiveFormsModule],
      providers: [{ provide: RazorpayService, useValue: mockRazorpayService }],
    }).compileComponents();

    fixture = TestBed.createComponent(PaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should mark form as invalid when empty', () => {
    expect(component.paymentForm.invalid).toBeTrue();
  });

  it('should validate contact number length (10 digits)', () => {
    const contact = component.contact;
    contact?.setValue('12345');
    expect(contact?.valid).toBeFalse();
    contact?.setValue('1234567890');
    expect(contact?.valid).toBeTrue();
  });

  it('should enable submit when form is valid', () => {
    component.paymentForm.setValue({
      amount: 100,
      name: 'Test User',
      email: 'test@example.com',
      contact: '9876543210'
    });
    expect(component.paymentForm.valid).toBeTrue();
  });

  it('should add to history after success', () => {
    component.addToHistory('pay_test_123', 500, 'success');
    expect(component.paymentHistory.length).toBe(1);
    expect(component.paymentHistory[0].id).toBe('pay_test_123');
  });

  it('should call RazorpayService when pay() is called with valid form', () => {
    const dummyFormData = {
      amount: 200,
      name: 'User',
      email: 'user@mail.com',
      contact: '9999999999'
    };

    component.paymentForm.setValue(dummyFormData);

    const mockOptions = {};
    mockRazorpayService.createRazorpayOptions.and.returnValue(mockOptions);

    component.pay();

    expect(mockRazorpayService.createRazorpayOptions).toHaveBeenCalledWith(
      dummyFormData,
      jasmine.any(Function),
      jasmine.any(Function)
    );
    expect(mockRazorpayService.launchRazorpay).toHaveBeenCalledWith(mockOptions);
  });
});
