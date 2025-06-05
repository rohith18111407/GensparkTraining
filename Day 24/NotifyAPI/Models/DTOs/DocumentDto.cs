namespace NotifyAPI.Models.DTOs
{
    public class DocumentDto
    {
        public string FileName { get; set; }
        public DateTime UploadedAt { get; set; }
        public string UploadedBy { get; set; }
    }
}