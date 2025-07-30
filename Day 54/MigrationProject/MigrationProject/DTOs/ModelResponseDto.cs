using System;

namespace MigrationProject.DTOs;

public class ModelResponseDto
{
    public int ModelId { get; set; }
    public string Model1 { get; set; }
    public List<ProductDto> Products { get; set; } = new();
}
