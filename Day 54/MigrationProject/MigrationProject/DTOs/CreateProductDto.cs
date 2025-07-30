using System;

namespace MigrationProject.DTOs;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public double Price { get; set; }

    public int CategoryId { get; set; }
    public int ColorId { get; set; }
    public int ModelId { get; set; }
}
