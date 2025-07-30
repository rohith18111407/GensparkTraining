using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigrationProject.Data;
using MigrationProject.DTOs;
using MigrationProject.Models;

namespace MigrationProject.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
                .ThenInclude(p => p.Color)
            .Include(c => c.Products)
                .ThenInclude(p => p.Model)
            .OrderBy(c => c.Name)
            .Select(c => new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Image = p.Image,
                    Price = p.Price,
                    SellStartDate = p.SellStartDate,
                    SellEndDate = p.SellEndDate,
                    IsNew = p.IsNew,
                    CategoryName = c.Name,
                    ColorName = p.Color.Color1,
                    ModelName = p.Model.Model1
                }).ToList()
            }).ToListAsync();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDto>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
                .ThenInclude(p => p.Color)
            .Include(c => c.Products)
                .ThenInclude(p => p.Model)
            .Where(c => c.CategoryId == id)
            .Select(c => new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Image = p.Image,
                    Price = p.Price,
                    SellStartDate = p.SellStartDate,
                    SellEndDate = p.SellEndDate,
                    IsNew = p.IsNew,
                    CategoryName = c.Name,
                    ColorName = p.Color.Color1,
                    ModelName = p.Model.Model1
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (category == null)
            return NotFound();

        return Ok(category);
    }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return NotFound();

            existingCategory.Name = categoryDto.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(categoryDto.Name))
                category.Name = categoryDto.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }