using System;
using System.ComponentModel.DataAnnotations;

namespace MigrationProject.Models;

public class Customer
{
    [Key]
    public int CustomerID { get; set; }
    [Required]
    public string CustomerName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
