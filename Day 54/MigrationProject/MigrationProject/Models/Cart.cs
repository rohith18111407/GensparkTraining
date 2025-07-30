using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationProject.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    // Parameterless constructor required by EF
    public Cart() { }

    public Cart(Product product, int quantity)
    {
        Product = product;
        ProductId = product.ProductId;
        Quantity = quantity;
    }
}
