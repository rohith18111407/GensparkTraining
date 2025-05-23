using System;
using FlyweightPattern.Forests;

namespace FlyweightPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var forest = new Forest();

            forest.PlantTree(10, 20, "Oak", "Green", "Rough");
            forest.PlantTree(15, 25, "Oak", "Green", "Rough");
            forest.PlantTree(30, 40, "Pine", "Dark Green", "Smooth");
            forest.PlantTree(40, 50, "Oak", "Green", "Rough");

            Console.WriteLine("Rendering Forest:");
            forest.DisplayForest();

            Console.ReadKey();
        }
    }
}
