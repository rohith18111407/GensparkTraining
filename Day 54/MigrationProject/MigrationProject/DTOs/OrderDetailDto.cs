using System;

namespace MigrationProject.DTOs;

public class OrderDetailDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double? Price { get; set; }
    public int Quantity { get; set; }
    public double? Total => Price * Quantity;
}
