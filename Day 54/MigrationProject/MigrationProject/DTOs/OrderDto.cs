using System;

namespace MigrationProject.DTOs;

public class OrderDto
{  
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public string PaymentType { get; set; }
    public string Status { get; set; }
    public List<OrderDetailDto> Items { get; set; } = new();
    public double? GrandTotal => Items.Sum(i => i.Total ?? 0);
}
