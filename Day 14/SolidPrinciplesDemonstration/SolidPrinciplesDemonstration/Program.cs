using SRP.Refactored;
using OCP.Refactored;
using LSP.Refactored;
using ISP.Refactored;
using DIP.Refactored;

/*
 * The SOLID Principles were introduced by a software engineer named Robert C. Martin 
 * (also known as "Uncle Bob") in the early 2000s. 
 * Uncle Bob’s goal was to promote good software design practices, 
 * particularly in object-oriented programming (OOP), 
 * by addressing common problems developers face 
 */

class Program
{
    static void Main()
    {
        var invoice = new Invoice(101, 5000);
        new InvoicePrinter().Print(invoice);
        new InvoiceSaver().Save(invoice);

        var processor = new InvoiceProcessor(new SeasonalDiscount());
        processor.Process(invoice.Amount);

        Bird parrot = new Parrot();
        Bird penguin = new Penguin();
        parrot.MakeSound();
        penguin.MakeSound();

        IPrinter printer = new SimplePrinter();
        printer.Print();
        var mfp = new MFP();
        mfp.Print();
        mfp.Scan();

        ILogger logger = new ConsoleLogger();
        var service = new InvoiceService(logger);
        service.GenerateInvoice("INV-101");
    }
}
