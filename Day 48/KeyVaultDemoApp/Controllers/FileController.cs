using KeyVaultDemoApp.Models.DTOs;
using KeyVaultDemoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyVaultDemoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly BlobStorageService _blobService;

    public FilesController(BlobStorageService blobService)
    {
        _blobService = blobService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadFileDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
            return BadRequest("No file provided");

        using var stream = dto.File.OpenReadStream();
        await _blobService.UploadAsync(stream, dto.File.FileName);
        return Ok($"Uploaded {dto.File.FileName}");
    }

    [HttpGet("download/{fileName}")]
    public async Task<IActionResult> Download(string fileName)
    {
        var stream = await _blobService.DownloadAsync(fileName);
        if (stream == null)
            return NotFound("File not found");

        return File(stream, "application/octet-stream", fileName);
    }
}
