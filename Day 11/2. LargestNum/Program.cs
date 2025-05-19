using System;

class LargestNum
{
    public static int FindLargest(int num1, int num2)
    {
        return ((num1 < num2) ? num2 : num1);
    }

    public static void Main(String[] args)
    {
        Console.WriteLine("Enter the number1: ");
        int num1 =int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the number2: ");
        int num2 = int.Parse(Console.ReadLine());

        int result = FindLargest(num1, num2);

        Console.WriteLine($"Largest number: {result} \n");

        Console.ReadKey();
    }
}