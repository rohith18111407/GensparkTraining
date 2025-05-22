using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP
{
    // Objects of a superclass should be replaceable with objects of a subclass
    // without affecting the correctness of the program.
    public class Bird
    {
        public virtual void Fly() => Console.WriteLine("Flying");
    }

    public class Penguin : Bird
    {
        public override void Fly() => throw new NotSupportedException("Penguins can't fly");
    }
}
