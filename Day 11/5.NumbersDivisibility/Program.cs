using System;

class DivisiblityCheck
{
    public static int Divisible(int divisible,int total)
    {
        int count = 0;

        Console.WriteLine($"Enter {total} numbers:");

        for (int i = 1; i <= total; i++)
        {
            Console.Write($"Enter number {i}: ");
            int num = int.Parse(Console.ReadLine());

            if (num % divisible == 0)
            {
                count++;
            }
        }

        return count;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the total count of numbers you want to enter: ");
        int total = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the divisibility number: ");
        int div = int.Parse(Console.ReadLine());

        int count =Divisible(div,total);
        Console.WriteLine($"\nTotal numbers divisible by 7: {count}");
        Console.ReadKey();
    }
}
