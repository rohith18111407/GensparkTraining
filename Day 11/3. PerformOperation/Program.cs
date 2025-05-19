using System;

class LargestNum
{
    public static int? FindLargest(int num1, int num2, string op)
    {
        int res;
        switch(op)
        {
            case "+":
                res = num1 + num2;
                break;
            case "-":
                res = num1 - num2;
                break;
            case "*": 
                res = num1 * num2;
                break;
            case "/":
                if (num2 != 0)
                    res = num1 / num2;
                else
                {
                    Console.WriteLine("Division by Zero!");
                    return null;
                }                   
                break;
            default:
                Console.WriteLine("Invalid Operation");
                return null;
        }
        return res;
    }

    public static void Main(String[] args)
    {
        Console.WriteLine("Enter the number1: ");
        int num1 = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the number2: ");
        int num2 = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter Operation: ");
        string op = Console.ReadLine();

        int? result = FindLargest(num1, num2, op);

        if(result.HasValue)
            Console.WriteLine($"Result: {result} \n");
        Console.ReadKey();
    }
}