using System;

class FrequencyCount
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

    public static Dictionary<int, int> CountFrequencies(int[] array)
    {
        Dictionary<int, int> frequencyMap = new Dictionary<int, int>();

        foreach(int num in array)
        {
            if(frequencyMap.ContainsKey(num))
            {
                frequencyMap[num]++;
            }
            else
            {
                frequencyMap[num] = 1;
            }
        }
        return frequencyMap;
    }

    public static void PrintFrequencies(Dictionary<int, int> freqMap)
    {
        Console.WriteLine("\nFrequency of each element:");
        foreach (var pair in freqMap)
        {
            Console.WriteLine($"{pair.Key} occurs {pair.Value} times");
        }
    }

    public static void Main(string[] args)
    {
        int[] inputArray = ReadArray();
        Dictionary<int, int> frequencyMap = CountFrequencies(inputArray);
        PrintFrequencies(frequencyMap);
        Console.ReadKey();
    }
}