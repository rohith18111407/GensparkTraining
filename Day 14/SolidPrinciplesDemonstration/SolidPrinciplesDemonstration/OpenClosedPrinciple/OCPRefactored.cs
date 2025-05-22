using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP.Refactored
{
    public interface IDiscount
    {
        double ApplyDiscount(double amount);
    }

    public class NoDiscount : IDiscount
    {
        public double ApplyDiscount(double amount) => amount;
    }

    public class SeasonalDiscount : IDiscount
    {
        public double ApplyDiscount(double amount) => amount * 0.9;
    }

    public class InvoiceProcessor
    {
        private readonly IDiscount _discount;
        public InvoiceProcessor(IDiscount discount) => _discount = discount;
        public void Process(double amount)
        {
            double finalAmount = _discount.ApplyDiscount(amount);
            Console.WriteLine($"Final amount: {finalAmount}");
        }
    }
}
