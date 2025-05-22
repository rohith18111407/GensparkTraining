using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.Refactored
{
    public interface IPrinter
    {
        void Print();
    }

    public interface IScanner
    {
        void Scan();
    }

    public class SimplePrinter : IPrinter
    {
        public void Print() => Console.WriteLine("Printing...");
    }

    public class MFP : IPrinter, IScanner
    {
        public void Print() => Console.WriteLine("Printing...");
        public void Scan() => Console.WriteLine("Scanning...");
    }
}