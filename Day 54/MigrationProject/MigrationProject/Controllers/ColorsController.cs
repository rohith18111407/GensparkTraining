using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigrationProject.Data;
using MigrationProject.Models;
using MigrationProject.DTOs;

namespace MigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColorResponseDto>>> GetColors()
        {
            var colors = await _context.Colors
                .Include(c => c.Products)
                    .ThenInclude(p => p.Category)
                .Include(c => c.Products)
                    .ThenInclude(p => p.Model)
                .ToListAsync();

            var colorDtos = colors.Select(color => new ColorResponseDto
            {
                ColorId = color.ColorId,
                Color1 = color.Color1,
                Products = color.Products.Select(product => new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Image = product.Image,
                    Price = product.Price,
                    SellStartDate = product.SellStartDate,
                    SellEndDate = product.SellEndDate,
                    IsNew = product.IsNew ?? 0,
                    CategoryName = product.Category?.Name ?? "Unknown",
                    ColorName = color.Color1, // from parent
                    ModelName = product.Model?.Model1 ?? "Unknown"
                }).ToList()
            });

            return Ok(colorDtos);
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorResponseDto>> GetColor(int id)
        {
            var color = await _context.Colors
                .Include(c => c.Products)
                    .ThenInclude(p => p.Category)
                .Include(c => c.Products)
                    .ThenInclude(p => p.Model)
                .FirstOrDefaultAsync(c => c.ColorId == id);

            if (color == null)
                return NotFound();

            var colorDto = new ColorResponseDto
            {
                ColorId = color.ColorId,
                Color1 = color.Color1,
                Products = color.Products.Select(product => new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Image = product.Image,
                    Price = product.Price,
                    SellStartDate = product.SellStartDate,
                    SellEndDate = product.SellEndDate,
                    IsNew = product.IsNew ?? 0,
                    CategoryName = product.Category?.Name ?? "Unknown",
                    ColorName = color.Color1,
                    ModelName = product.Model?.Model1 ?? "Unknown"
                }).ToList()
            };

            return Ok(colorDto);
        }

        // POST: api/Colors
        [HttpPost]
        public async Task<ActionResult<Color>> CreateColor([FromBody] ColorDto dto)
        {
            var color = new Color
            {
                Color1 = dto.Color1
            };

            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetColor), new { id = color.ColorId }, dto);
        }

        // PUT: api/Colors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateColor(int id, [FromBody] ColorDto dto)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
                return NotFound();

            color.Color1 = dto.Color1;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Colors/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchColor(int id, [FromBody] ColorDto dto)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(dto.Color1))
                color.Color1 = dto.Color1;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
                return NotFound();

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
