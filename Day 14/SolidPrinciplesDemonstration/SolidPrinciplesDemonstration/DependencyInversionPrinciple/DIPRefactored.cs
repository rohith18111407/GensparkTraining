using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP.Refactored
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine($"[Log]: {message}");
    }

    public class InvoiceService
    {
        private readonly ILogger _logger;
        public InvoiceService(ILogger logger) => _logger = logger;
        public void GenerateInvoice(string id)
        {
            _logger.Log($"Generated invoice {id}");
        }
    }
}
