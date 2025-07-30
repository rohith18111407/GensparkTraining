using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigrationProject.Data;
using MigrationProject.DTOs;
using MigrationProject.Models;

namespace MigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelDto>>> GetModels()
        {
            var models = await _context.Models
                .ToListAsync();

            var modelDtos = models.Select(m => new ModelDto
            {
                ModelId = m.ModelId,
                Model1 = m.Model1
            }).ToList();

            return Ok(modelDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModelResponseDto>> GetModel(int id)
        {
            var model = await _context.Models
                .Include(m => m.Products)
                .FirstOrDefaultAsync(m => m.ModelId == id);

            if (model == null)
                return NotFound();

            var modelDto = new ModelResponseDto
            {
                ModelId = model.ModelId,
                Model1 = model.Model1,
                Products = model.Products?.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price
                }).ToList() ?? new()
            };

            return Ok(modelDto);
        }

        [HttpPost]
        public async Task<ActionResult<ModelDto>> CreateModel([FromBody] ModelDto modelDto)
        {
            var model = new Model
            {
                Model1 = modelDto.Model1
            };

            _context.Models.Add(model);
            await _context.SaveChangesAsync();

            modelDto.ModelId = model.ModelId;

            return CreatedAtAction(nameof(GetModel), new { id = model.ModelId }, modelDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] ModelDto modelDto)
        {
            var existingModel = await _context.Models.FindAsync(id);
            if (existingModel == null)
                return NotFound();

            existingModel.Model1 = modelDto.Model1;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
                return NotFound();

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
