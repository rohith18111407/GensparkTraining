using System;
using System.Collections.Generic;

class ProductDictionary
{
    static void Main()
    {
        // Create the Dictionary
        Dictionary<string, double> productPrices = new Dictionary<string, double>();

        // Add 5 products
        productPrices.Add("Laptop", 75000.00);
        productPrices.Add("Smartphone", 25000.50);
        productPrices.Add("Tablet", 18000.75);
        productPrices.Add("Headphones", 2999.99);
        productPrices.Add("Smartwatch", 9999.00);

        // Display all key-value pairs
        Console.WriteLine("Available Products and Prices:\n");
        foreach (var item in productPrices)
        {
            Console.WriteLine($"Product: {item.Key}, Price: ₹{item.Value}");
        }

        Console.WriteLine("\nEnter the product name to search for its price:");
        string searchProduct = Console.ReadLine();

        // Search for the product using ContainsKey
        if (productPrices.ContainsKey(searchProduct))
        {
            Console.WriteLine($"The price of {searchProduct} is ₹{productPrices[searchProduct]}");
        }
        else
        {
            Console.WriteLine($"Product '{searchProduct}' not found in the list.");
        }

        Console.ReadKey();
    }
}
