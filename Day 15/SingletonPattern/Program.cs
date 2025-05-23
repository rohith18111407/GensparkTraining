using System;
using System.IO;

namespace SingletonPattern
{
    /*
     * Definition: Ensures a class has only one instance and provides a global point of access to it.
     */

    public class FileManager
    {
        private static FileManager _instance;
        private StreamWriter _writer;
        private StreamReader _reader;
        private FileStream _stream;
        public string FilePath { get; private set; }

        private FileManager(string path)
        {
            FilePath = path;
            _stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _writer = new StreamWriter(_stream);
            _reader = new StreamReader(_stream);
        }

        public static FileManager GetInstance(string path)
        {
            if (_instance == null)
            {
                _instance = new FileManager(path);
            }
            return _instance;
        }

        public void Write(string text)
        {
            _writer.WriteLine(text);
            _writer.Flush();
        }

        public string ReadAll()
        {
            _stream.Seek(0, SeekOrigin.Begin);
            return _reader.ReadToEnd();
        }

        public void Close()
        {
            _writer?.Close();
            _reader?.Close();
            _stream?.Close();
        }
    }

    class Program
    {
        static void Main()
        {
            var fileManager = FileManager.GetInstance("singleton.txt");
            fileManager.Write("Singleton pattern writing to file.");
            Console.WriteLine("Reading from file:");
            Console.WriteLine(fileManager.ReadAll());
            fileManager.Close();

            Console.ReadKey();
        }
    }
}