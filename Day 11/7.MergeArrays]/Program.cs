using System;

class MergeArrays
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

    public static int[] Merge(int[] inputArray1, int[] inputArray2)
    {
        int[] outputArray = new int[inputArray1.Length + inputArray2.Length];

        Array.Copy(inputArray1, 0, outputArray, 0, inputArray1.Length);
        Array.Copy(inputArray2, 0, outputArray, inputArray1.Length, inputArray2.Length);

        return outputArray;
    }

    public static void PrintArray(int[] array)
    {
        Console.WriteLine(" Merged Array is { "+ string.Join(",",array) + " }");
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Array 1:\n");
        int[] inputArray1 = ReadArray();

        Console.WriteLine("Array 2:\n");
        int[] inputArray2 = ReadArray();

        int[] outputArray = Merge(inputArray1,inputArray2);

        PrintArray(outputArray);

        Console.ReadLine();
    }
}