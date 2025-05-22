using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP
{
    // High-level modules should not depend on low-level modules;
    // both should depend on abstractions.
    public class InvoiceLogger
    {
        public void Log(string message) => Console.WriteLine($"[Log]: {message}");
    }

    public class InvoiceService
    {
        private InvoiceLogger _logger = new InvoiceLogger();
        public void GenerateInvoice(string id)
        {
            _logger.Log($"Generated invoice {id}");
        }
    }
}
