using System;
using SecureFileAccess.Enums;
using SecureFileAccess.Interfaces;
using SecureFileAccess.Models;

namespace SecureFileAccess.Core
{
    public class ProxyFile : IFile
    {
        private File _realFile;
        private User _user;

        public ProxyFile(User user, string content)
        {
            _user = user;
            _realFile = new File(content);
        }

        public void Read()
        {
            switch (_user.UserRole)
            {
                case Role.Admin:
                    _realFile.Read();
                    break;

                case Role.User:
                    Console.WriteLine("\nLimited Access given\n  view file's structure only will be visible.");
                    break;

                case Role.Guest:
                    Console.WriteLine("\nAccess Denied\n You do not have permission to read this file.");
                    break;

                default:
                    Console.WriteLine("\nError\n Invalid role.");
                    break;
            }
        }
    }
}
