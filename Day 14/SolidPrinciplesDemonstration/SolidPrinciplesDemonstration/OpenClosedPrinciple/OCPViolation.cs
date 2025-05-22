using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP
{
    // Software entities should be open for extension but closed for modification.
    public class Invoice
    {
        public int Id { get; set; }
        public double Amount { get; set; }
    }

    public class InvoiceProcessor
    {
        public void Process(Invoice invoice, string discountType)
        {
            double finalAmount = invoice.Amount;
            if (discountType == "Seasonal") 
                finalAmount *= 0.9;
            else if (discountType == "None") 
                finalAmount *= 1;
            Console.WriteLine($"Final amount: {finalAmount}");
        }
    }
}

