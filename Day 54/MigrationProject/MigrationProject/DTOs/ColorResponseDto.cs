using System;

namespace MigrationProject.DTOs;

public class ColorResponseDto
{
    public int ColorId { get; set; }
    public string Color1 { get; set; }

    public List<ProductDto> Products { get; set; } = new();
}
