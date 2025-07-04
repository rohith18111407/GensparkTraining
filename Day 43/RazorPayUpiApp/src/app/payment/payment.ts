import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RazorpayService } from '../services/razorpay.service';
import { PaymentRecord } from '../models/paymentrecord.model';


@Component({
  selector: 'app-payment',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './payment.html',
  styleUrl: './payment.css'
})
export class PaymentComponent{
  paymentForm: FormGroup;
  paymentStatus: string | null = null;
  paymentId: string | null = null;
  paymentHistory: PaymentRecord[] = [];

  constructor(private fb: FormBuilder, private razorpayService: RazorpayService) {
    this.paymentForm = this.fb.group({
      amount: ['', [Validators.required, Validators.min(1)]],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      contact: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]]
    });
  }

  get amount() { return this.paymentForm.get('amount'); }
  get name() { return this.paymentForm.get('name'); }
  get email() { return this.paymentForm.get('email'); }
  get contact() { return this.paymentForm.get('contact'); }

  pay(): void {
    if (this.paymentForm.invalid) return;

    const formData = this.paymentForm.value;
    const options = this.razorpayService.createRazorpayOptions(
      formData,
      (response: any) => {
        this.paymentStatus = 'success';
        this.paymentId = response.razorpay_payment_id;
        this.addToHistory(this.paymentId!, formData.amount, 'success');
      },
      () => {
        this.paymentStatus = 'cancelled';
        this.addToHistory('N/A', formData.amount, 'cancelled');
      }
    );

    this.razorpayService.launchRazorpay(options);
  }

  addToHistory(id: string, amount: number, status: 'success' | 'cancelled') {
    this.paymentHistory.unshift({ id, amount, status, date: new Date() });
  }
}
