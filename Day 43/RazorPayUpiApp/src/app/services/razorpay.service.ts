import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class RazorpayService {
  private razorpayKey = 'rzp_test_4Zizm5YbrAr5ZO';

  createRazorpayOptions(data: any, successCallback: Function, cancelCallback: Function): any {
    return {
      key: this.razorpayKey,
      amount: data.amount * 100,
      currency: 'INR',
      name: data.name,
      description: 'UPI Payment Test',
      prefill: {
        name: data.name,
        email: data.email,
        contact: data.contact
      },
      method: {
        upi: true,
        netbanking: true,
        card: true,
        wallet: true
      },
      theme: {
        color: '#007bff'
      },
      handler: (response: any) => successCallback(response),
      modal: {
        ondismiss: () => cancelCallback()
      }
    };
  }

  launchRazorpay(options: any): void {
    const rzp = new (window as any).Razorpay(options);
    rzp.open();
  }
}