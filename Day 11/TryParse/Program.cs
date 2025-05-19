using System;

namespace TryParse
{
    internal class Program
    {
        int num1, num2;
        void GetNumbersFromUser()
        {
            Console.WriteLine("Please enter the first number");
            //bool isConverted = int.TryParse(Console.ReadLine(), out num1);
            // if(isConverted)
            // {
            //     Console.WriteLine($"The number is {num1}");
            // }
            // else
            // {
            //     Console.WriteLine("Invalid input");
            // }
            //Console.WriteLine($"The incremented number is {++num1}");
            while (!int.TryParse(Console.ReadLine(), out num1))
                Console.WriteLine("Invalid input. Please try again");
            Console.WriteLine($"The incremented number is {++num1}");
          
        }
        static void Main(string[] args)
        {
            new Program().GetNumbersFromUser();
            Console.ReadKey();
        }
    }
}