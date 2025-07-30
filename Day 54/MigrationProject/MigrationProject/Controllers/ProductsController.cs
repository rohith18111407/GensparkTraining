using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigrationProject.Data;
using MigrationProject.DTOs;
using MigrationProject.Models;

namespace MigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _context.Categories.FindAsync(dto.CategoryId);
            var color = await _context.Colors.FindAsync(dto.ColorId);
            var model = await _context.Models.FindAsync(dto.ModelId);

            if (category == null || color == null || model == null)
                return NotFound("One or more referenced entities (Category, Color, Model) were not found.");

            var product = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                ColorId = dto.ColorId,
                ModelId = dto.ModelId,
                SellStartDate = DateTime.UtcNow,
                IsNew = 1
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Create and return ProductDto
            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                SellStartDate = product.SellStartDate,
                SellEndDate = product.SellEndDate,
                IsNew = product.IsNew ,
                CategoryName = category.Name,
                ColorName = color.Color1,
                ModelName = model.Model1
            };

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, productDto);
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Model)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            var dto = new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Image = product.Image,
                Price = product.Price,
                SellStartDate = product.SellStartDate,
                SellEndDate = product.SellEndDate,
                IsNew = product.IsNew,
                CategoryName = product.Category?.Name,
                ColorName = product.Color?.Color1,
                ModelName = product.Model?.Model1
            };

            return Ok(dto);
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Model)
                .ToListAsync();

            var productDtos = products.Select(product => new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Image = product.Image,
                Price = product.Price,
                SellStartDate = product.SellStartDate,
                SellEndDate = product.SellEndDate,
                IsNew = product.IsNew,
                CategoryName = product.Category?.Name ?? string.Empty,
                ColorName = product.Color?.Color1 ?? string.Empty,
                ModelName = product.Model?.Model1 ?? string.Empty
            }).ToList();

            return Ok(productDtos);
        }
    }
}
