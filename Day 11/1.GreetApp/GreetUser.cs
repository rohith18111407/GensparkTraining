using System;

class GreetUser
{
    public static void Greetings(string? name)
    {
        if (!string.IsNullOrEmpty(name))
            Console.WriteLine($"Hello, {name} Have a great day.");
        else
            Console.WriteLine("Valid string is required");
    }

    public static void Main()
    {
        Console.Write("Hi, Enter your name: ");
        string? name = Console.ReadLine();
        Greetings(name);
        Console.ReadKey();
    }
}
