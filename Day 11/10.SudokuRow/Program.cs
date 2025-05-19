using System;

class SudokuRow
{
    public static int[] ReadArray()
    {
        int size = 9;
        //Console.WriteLine("Pls enter the total num of elements in array: ");
        //while (!int.TryParse(Console.ReadLine(), out size))
        //    Console.WriteLine("Invalid input. Pls try again! ");

        int[] array = new int[size];

        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"Pls enter element {i + 1}: ");
            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || value < 1 || value > 9 )
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
            }
            array[i] = value;
        }
        return array;
    }

    public static void PrintArray(int[] array)
    {
        Console.WriteLine("Output Array is { " + string.Join(",", array) + " }");
    }

    public static bool isValid(int[] array)
    {
        bool[] seen = new bool[10];

        foreach (int num in array)
        {
            if(seen[num] == false && (num >= 1 || num <=10) )
                seen[num] = true;
            else
                return false;
        }
        return true;
    }

    public static void Main(string[] args)
    {
        int[] inputArray = ReadArray();

        bool isvalid = isValid(inputArray);

        if(isvalid )
        {
            PrintArray(inputArray);
            Console.WriteLine("\n It is valid\n");
        }
        else
        {
            PrintArray(inputArray);
            Console.WriteLine("\n It is invalid\n");
        }

            Console.ReadKey();
    }
}