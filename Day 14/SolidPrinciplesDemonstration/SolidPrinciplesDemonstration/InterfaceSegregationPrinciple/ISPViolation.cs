using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP
{
    // Clients should not be forced to depend on interfaces they do not use
    public interface IMachine
    {
        void Print();
        void Scan();
    }

    public class OldPrinter : IMachine
    {
        public void Print() => Console.WriteLine("Printing...");
        public void Scan() => throw new NotImplementedException();
    }
}

