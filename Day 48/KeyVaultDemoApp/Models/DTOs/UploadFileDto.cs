using Microsoft.AspNetCore.Http;

namespace KeyVaultDemoApp.Models.DTOs;

public class UploadFileDto
{
    public IFormFile File { get; set; }
}
