using System;
using System.IO;

namespace FactoryMethodPattern
{
    /*
     * Definition: Defines an interface for creating an object 
     * but lets subclasses alter the type of objects that will be created.

     */


    public interface IFileOperation
    {
        void Execute();
    }

    public class FileWriter : IFileOperation
    {
        private string _path;
        private string _text;
        public FileWriter(string path, string text) => (_path, _text) = (path, text);
        public void Execute()
        {
            using (var writer = new StreamWriter(_path, append: true))
            {
                writer.WriteLine(_text);
            }
            Console.WriteLine("Written: " + _text);
        }
    }

    public class FileReader : IFileOperation
    {
        private string _path;
        public FileReader(string path) => _path = path;
        public void Execute()
        {
            using (var reader = new StreamReader(_path))
            {
                Console.WriteLine("Reading content:\n" + reader.ReadToEnd());
            }
        }
    }

    public abstract class FileOperationFactory
    {
        public abstract IFileOperation CreateOperation(string path, string text = null);
    }

    public class WriterFactory : FileOperationFactory
    {
        public override IFileOperation CreateOperation(string path, string text)
        {
            return new FileWriter(path, text);
        }
    }

    public class ReaderFactory : FileOperationFactory
    {
        public override IFileOperation CreateOperation(string path, string text = null)
        {
            return new FileReader(path);
        }
    }

    class Program
    {
        static void Main()
        {
            var path = "factory.txt";
            var writerFactory = new WriterFactory();
            var writer = writerFactory.CreateOperation(path, "Factory Method Pattern example");
            writer.Execute();

            var readerFactory = new ReaderFactory();
            var reader = readerFactory.CreateOperation(path);
            reader.Execute();

            Console.ReadKey();
        }
    }
}
