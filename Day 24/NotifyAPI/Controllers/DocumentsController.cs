using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotifyAPI.Interfaces;
using NotifyAPI.Models.DTOs;

namespace NotifyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository documentRepository;

        public DocumentsController(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        // POST: api/Documents/Upload
        [HttpPost("Upload")]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Upload([FromForm] DocumentUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded");

            await documentRepository.UploadAsync(dto.File, User);
            return Ok("Document uploaded successfully");
        }

        // GET: api/Documents
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAll()
        {
            var docs = await documentRepository.GetAllAsync();

            var response = docs.Select(d => new DocumentDto
            {
                FileName = d.FileName,
                UploadedAt = d.UploadedAt,
                UploadedBy = d.UploadedBy
            }).ToList();

            return Ok(response);
        }
    }
}