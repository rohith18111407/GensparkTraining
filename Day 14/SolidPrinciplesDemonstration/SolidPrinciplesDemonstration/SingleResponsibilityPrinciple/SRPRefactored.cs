using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRP.Refactored
{
    public class Invoice
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public Invoice(int id, double amount)
        {
            Id = id;
            Amount = amount;
        }
    }

    public class InvoicePrinter
    {
        public void Print(Invoice invoice) => Console.WriteLine($"Invoice ID: {invoice.Id}, Amount: {invoice.Amount}");
    }

    public class InvoiceSaver
    {
        public void Save(Invoice invoice) => Console.WriteLine($"Saving Invoice {invoice.Id}");
    }
}
