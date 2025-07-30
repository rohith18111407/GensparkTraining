using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationProject.Models;

public class Order
{
    public int OrderID { get; set; }
    public string OrderName { get; set; }
    public DateTime? OrderDate { get; set; }
    public string PaymentType { get; set; }
    public string Status { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public int? TotalAmount { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; } = new HashSet<OrderDetail>();
}
