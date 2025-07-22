using Microsoft.AspNetCore.Http;

namespace TrainingVideoPortalAPI.Models.DTOs
{
    public class TrainingVideoUploadRequestDto
    {
        public IFormFile File { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}