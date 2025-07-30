using System;

namespace MigrationProject.Models;

public class Model
{
    public int ModelId { get; set; }
    public string Model1 { get; set; }
    public virtual ICollection<Product>? Products { get; set; } = new HashSet<Product>();
}
