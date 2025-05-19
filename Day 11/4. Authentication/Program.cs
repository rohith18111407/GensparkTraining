using System;

class LoginSystem
{
    const int maxAttempts = 3;
    const string correctUsername = "Admin";
    const string correctPassword = "pass";

    public static bool Authenticate()
    {
        int attempts = 0;
        while (attempts < 3)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (username == correctUsername && password == correctPassword)
            {
                return true;
            }
            else
            {
                attempts++;
                Console.WriteLine($"Invalid credentials. Attempts left: {3 - attempts}\n");
            }
        }
        return false;
    }

    public static void Main(string[] args)
    {
        bool success = Authenticate();

        if (success)
        {
            Console.WriteLine("Login Successful!");
        }
        else
        {
            Console.WriteLine("Invalid attempts for 3 times. Exiting....");
        }
            
        Console.ReadKey();
    }
}
