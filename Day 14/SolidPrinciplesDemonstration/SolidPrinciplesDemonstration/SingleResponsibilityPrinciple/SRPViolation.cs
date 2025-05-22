using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRP
{
    // A class should have only one reason to change,
    // meaning it should have only one job or responsibility
    public class Invoice
    {
        public int Id { get; set; }
        public double Amount { get; set; }

        public void Print() => Console.WriteLine($"Printing Invoice {Id}");
        public void Save() => Console.WriteLine($"Saving Invoice {Id}");
    }
}
