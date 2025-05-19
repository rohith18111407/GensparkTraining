using System;

class RotateArrays
{
    public static int[] ReadArray()
    {
        int size;
        Console.WriteLine("Pls enter the total num of elements in array: ");
        while (!int.TryParse(Console.ReadLine(), out size))
            Console.WriteLine("Invalid input. Pls try again! ");

        int[] array = new int[size];

        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"Pls enter element {i + 1}: ");
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
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

    public static int[] Rotate(int[] array)
    {
        int firstElement = array[0];

        for(int i = 1; i < array.Length;i++)
        {
            array[i-1] = array[i];
        }

        array[array.Length-1] = firstElement;
        return array;
    }

    public static void Main(string[] args)
    {
        int[] inputArray = ReadArray();

        inputArray = Rotate(inputArray);

        PrintArray(inputArray);

        Console.ReadKey();

    }
}