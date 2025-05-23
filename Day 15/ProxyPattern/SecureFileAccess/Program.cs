using System;
using SecureFileAccess.Enums;
using SecureFileAccess.Interfaces;
using SecureFileAccess.Models;
using SecureFileAccess.Core;

namespace SecureFileAccess
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Secure File Access System ===\n");

            Console.Write("Enter your name: ");
            string username = Console.ReadLine();

            Console.Write("Enter your role (Admin/User/Guest): ");
            string roleInput = Console.ReadLine();

            if (!Enum.TryParse(roleInput, true, out Role userRole))
            {
                Console.WriteLine("[Error] Invalid role. Please enter Admin, User, or Guest.");
                return;
            }

            var user = new User(username, userRole);
            string fileContent = "Sensitive content: National Security Report";

            Console.WriteLine($"\nUser: {user.Username} | Role: {user.UserRole}");
            IFile secureFile = new ProxyFile(user, fileContent);
            secureFile.Read();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
