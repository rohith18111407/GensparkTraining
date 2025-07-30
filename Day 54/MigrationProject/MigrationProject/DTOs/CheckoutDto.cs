using System;

namespace MigrationProject.DTOs;

public class CheckoutDto
{
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public string? PaymentType { get; set; } = "Cash";
    public List<CartDto> Items { get; set; } = new();
}
