using System;
using System.IO;

namespace AbstractFactoryPattern
{
    /*
     * Definition: Provides an interface for creating families of related or dependent objects 
     * without specifying their concrete classes.

     */

    public interface IFileReader
    {
        void Read();
    }

    public interface IFileWriter
    {
        void Write(string text);
    }

    public interface IFileFactory
    {
        IFileReader CreateReader(string path);
        IFileWriter CreateWriter(string path);
    }

    public class TextFileReader : IFileReader
    {
        private string _path;
        public TextFileReader(string path) => _path = path;
        public void Read()
        {
            using var reader = new StreamReader(_path);
            Console.WriteLine("[TextFileReader]:\n" + reader.ReadToEnd());
        }
    }

    public class TextFileWriter : IFileWriter
    {
        private string _path;
        public TextFileWriter(string path) => _path = path;
        public void Write(string text)
        {
            using var writer = new StreamWriter(_path, append: true);
            writer.WriteLine(text);
            Console.WriteLine("[TextFileWriter]: " + text);
        }
    }

    public class TextFileFactory : IFileFactory
    {
        public IFileReader CreateReader(string path) => new TextFileReader(path);
        public IFileWriter CreateWriter(string path) => new TextFileWriter(path);
    }

    class Program
    {
        static void Main()
        {
            IFileFactory factory = new TextFileFactory();
            string path = "abstractfactory.txt";

            var writer = factory.CreateWriter(path);
            writer.Write("Abstract Factory Pattern Line 1");
            writer.Write("Abstract Factory Pattern Line 2");

            var reader = factory.CreateReader(path);
            reader.Read();

            Console.ReadKey();
        }
    }
}
