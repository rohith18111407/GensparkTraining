export interface PaymentRecord {
  id: string;                      // Razorpay Payment ID or 'N/A' if cancelled
  amount: number;                  // Payment amount in INR
  status: 'success' | 'cancelled'; // Payment status
  date: Date;                      // Timestamp of the transaction
}
