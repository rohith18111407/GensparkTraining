using System;

namespace MigrationProject.DTOs;

public class CategoryResponseDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public List<ProductDto> Products { get; set; } = new();
}
