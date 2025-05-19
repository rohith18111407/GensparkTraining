using System;
using System.Security.Authentication;

class EncryptDecrypt
{
    public static string Encrypt(string text, int shift)
    {
        string result = "";

        foreach (char ch in text)
        {
            if (ch >= 'a' && ch <= 'z')
            {
                char encryptedChar = (char)('a' + (ch - 'a' + shift) % 26);
                result += encryptedChar;
            }
            else
            {
                Console.WriteLine("Invalid character found. Only lowercase letters allowed.");
                return "";
            }
        }

        return result;
    }

    public static string Decrypt(string encrypted, int shift)
    {
        string result = "";

        foreach (char ch in encrypted)
        {
            if (ch >= 'a' && ch <= 'z')
            {
                char decryptedChar = (char)('a' + (ch - 'a' - shift + 26) % 26);
                result += decryptedChar;
            }
            else
            {
                Console.WriteLine("Invalid character found. Only lowercase letters allowed.");
                return "";
            }
        }

        return result;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Please enter the text in lowercase: ");
        string text = Console.ReadLine();

        int shift;
        Console.WriteLine("Please enter the shift: ");
        while(!int.TryParse(Console.ReadLine(),out shift) || shift < 0)
        {
            Console.WriteLine("Please enter positve shift value");
        }

        string encrypted = Encrypt(text,shift);
        string decrypted = Decrypt(encrypted,shift);

        Console.WriteLine($"Encrypted Text: {encrypted}");
        Console.WriteLine($"Decrypted Text: {decrypted}");

        Console.ReadKey();
    }
}