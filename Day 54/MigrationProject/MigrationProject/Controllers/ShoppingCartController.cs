using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigrationProject.Data;
using MigrationProject.DTOs;
using MigrationProject.Models;

namespace MigrationProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ShoppingCartController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/ShoppingCart/checkout
    [HttpPost("checkout")]
    public async Task<ActionResult> Checkout([FromBody] CheckoutDto checkout)
    {
        if (checkout == null || checkout.Items == null || !checkout.Items.Any())
            return BadRequest("Cart is empty.");

        var order = new Order
        {
            OrderName = $"Order_{DateTime.UtcNow:yyyyMMddHHmmss}",
            CustomerName = checkout.CustomerName,
            CustomerPhone = checkout.CustomerPhone,
            CustomerEmail = checkout.CustomerEmail,
            CustomerAddress = checkout.CustomerAddress,
            OrderDate = DateTime.UtcNow,
            PaymentType = checkout.PaymentType ?? "Cash",
            Status = "Processing"
        };



        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        foreach (var item in checkout.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null)
                return NotFound($"Product ID {item.ProductId} not found.");

            var detail = new OrderDetail
            {
                OrderID = order.OrderID,
                ProductID = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price
            };
            _context.OrderDetails.Add(detail);
        }

        await _context.SaveChangesAsync();
        return Ok(new { message = "Order placed successfully.", orderId = order.OrderID });
    }

    // GET: api/ShoppingCart/orders
    [HttpGet("orders")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();

        var result = orders.Select(o => new OrderDto
        {
            OrderId = o.OrderID,
            CustomerName = o.CustomerName,
            CustomerPhone = o.CustomerPhone,
            CustomerEmail = o.CustomerEmail,
            CustomerAddress = o.CustomerAddress,
            OrderDate = o.OrderDate.GetValueOrDefault(),
            PaymentType = o.PaymentType,
            Status = o.Status,
            Items = o.OrderDetails.Select(d => new OrderDetailDto
            {
                ProductId = d.ProductID,
                ProductName = d.Product?.ProductName ?? "Unknown",
                Price = d.Price,
                Quantity = d.Quantity
            }).ToList()
        });

        return Ok(result);
    }

    // GET: api/ShoppingCart/orders/{id}
    [HttpGet("orders/{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.OrderID == id);

        if (order == null)
            return NotFound($"Order ID {id} not found.");

        var dto = new OrderDto
        {
            OrderId = order.OrderID,
            CustomerName = order.CustomerName,
            CustomerPhone = order.CustomerPhone,
            CustomerEmail = order.CustomerEmail,
            CustomerAddress = order.CustomerAddress,
            OrderDate = order.OrderDate.GetValueOrDefault(),
            PaymentType = order.PaymentType,
            Status = order.Status,
            Items = order.OrderDetails.Select(d => new OrderDetailDto
            {
                ProductId = d.ProductID,
                ProductName = d.Product?.ProductName ?? "Unknown",
                Price = d.Price,
                Quantity = d.Quantity
            }).ToList()
        };

        return Ok(dto);
    }
}
