using System;

class BullsAndCows
{
    const string SECRET_WORD = "GAME";

    public static (int bulls, int cows) GetBullsAndCows(string guess, string secret)
    {
        int bulls = 0, cows = 0;
        bool[] secretUsed = new bool[secret.Length];
        bool[] guessUsed = new bool[guess.Length];

        // Count Bulls
        for (int i = 0; i < secret.Length; i++)
        {
            if (guess[i] == secret[i])
            {
                bulls++;
                secretUsed[i] = true;
                guessUsed[i] = true;
            }
        }

        // Count Cows
        for (int i = 0; i < guess.Length; i++)
        {
            if (guessUsed[i]) 
                continue;
            for (int j = 0; j < secret.Length; j++)
            {
                if (!secretUsed[j] && guess[i] == secret[j])
                {
                    cows++;
                    secretUsed[j] = true;
                    break;
                }
            }
        }

        return (bulls, cows);
    }

    public static void Main(string[] args)
    {
        int attempts = 0;

        Console.WriteLine("Welcome to the Bulls and Cows Game!");
        Console.WriteLine("Guess the 4-letter word. (Secret word is fixed)");

        while (true)
        {
            Console.Write("\nEnter your 4-letter guess: ");
            string guess = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrWhiteSpace(guess) || guess.Length != 4)
            {
                Console.WriteLine("Invalid input. Enter a 4-letter word.");
                continue;
            }

            attempts++;

            var (bulls, cows) = GetBullsAndCows(guess, SECRET_WORD);

            Console.WriteLine($"{bulls} Bulls, {cows} Cows");

            if (bulls == 4)
            {
                Console.WriteLine($" Congratulations! You've guessed the word in {attempts} attempts.");
                break;
            }
        }

        Console.ReadKey();
    }
}
