using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP.Refactored
{
    // Base class for all birds
    public abstract class Bird
    {
        public abstract void MakeSound();
    }

    // Interface for birds that can fly
    public interface IFlyable
    {
        void Fly();
    }

    public class Parrot : Bird, IFlyable
    {
        public override void MakeSound() => Console.WriteLine("Parrot says: Squawk");
        public void Fly() => Console.WriteLine("Parrot is flying");
    }

    public class Penguin : Bird
    {
        public override void MakeSound() => Console.WriteLine("Penguin says: Honk");
        // No Fly method here — Penguins do not fly
    }
}