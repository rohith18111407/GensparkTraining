using System;

namespace MigrationProject.Models;

public class Color
{
    public int ColorId { get; set; }
    public string Color1 { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
