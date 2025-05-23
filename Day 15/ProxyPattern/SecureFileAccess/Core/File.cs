using System;
using SecureFileAccess.Interfaces;

namespace SecureFileAccess.Core
{
    public class File : IFile
    {
        private string _content;

        public File(string content)
        {
            _content = content;
        }

        public void Read()
        {
            Console.WriteLine($"\nAccess Granted\n Reading sensitive file content...\n{_content}");
        }
    }
}
