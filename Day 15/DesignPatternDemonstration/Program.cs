using System;
using System.IO;

// Singleton: Ensures only one file stream
public class FileManager
{
    private static FileManager _instance;
    private static readonly object _lock = new object();
    private StreamWriter _writer;
    private StreamReader _reader;
    public string FilePath { get; }

    private FileManager(string path)
    {
        FilePath = path;
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        _writer = new StreamWriter(stream);
        _reader = new StreamReader(stream);
    }

    public static FileManager GetInstance(string path)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new FileManager(path);
            }
        }
        return _instance;
    }

    public StreamWriter GetWriter() => _writer;
    public StreamReader GetReader() => _reader;

    public void Close()
    {
        _writer?.Flush();
        _writer?.Close();
        _reader?.Close();
    }
}

// Factory Method: File operations interface
public interface IFileOperation
{
    void Execute();
}

// Concrete Reader
public class FileReader : IFileOperation
{
    private FileManager _fileManager;
    public FileReader(FileManager manager)
    {
        _fileManager = manager;
    }

    public void Execute()
    {
        _fileManager.GetReader().BaseStream.Seek(0, SeekOrigin.Begin); // Reset pointer
        string content = _fileManager.GetReader().ReadToEnd();
        Console.WriteLine("Reading from file:\n" + content);
    }
}

// Concrete Writer
public class FileWriter : IFileOperation
{
    private FileManager _fileManager;
    private string _text;
    public FileWriter(FileManager manager, string text)
    {
        _fileManager = manager;
        _text = text;
    }

    public void Execute()
    {
        _fileManager.GetWriter().WriteLine(_text);
        _fileManager.GetWriter().Flush(); // Ensure it's written immediately
        Console.WriteLine("Written to file: " + _text);
    }
}



// Abstract Factory
public interface IFileOperationFactory
{
    IFileOperation CreateReader(FileManager manager);
    IFileOperation CreateWriter(FileManager manager, string text);
}

// Concrete Factory for Text Files
public class TextFileOperationFactory : IFileOperationFactory
{
    public IFileOperation CreateReader(FileManager manager)
    {
        return new FileReader(manager);
    }

    public IFileOperation CreateWriter(FileManager manager, string text)
    {
        return new FileWriter(manager, text);
    }
}




class Program
{
    static void Main(string[] args)
    {
        string path = "data.txt";
        FileManager fileManager = FileManager.GetInstance(path);

        IFileOperationFactory factory = new TextFileOperationFactory();

        // Write operation
        IFileOperation writer = factory.CreateWriter(fileManager, "Hello, this is a test line.");
        writer.Execute();

        // Write another line
        writer = factory.CreateWriter(fileManager, "Another line.");
        writer.Execute();

        // Read operation
        IFileOperation reader = factory.CreateReader(fileManager);
        reader.Execute();

        // Ensure the file is closed once
        fileManager.Close();
        Console.WriteLine("File closed successfully.");
    }
}
