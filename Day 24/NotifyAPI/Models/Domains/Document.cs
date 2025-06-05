using System.ComponentModel.DataAnnotations;

namespace NotifyAPI.Models.Domains
{
    public class Document
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public DateTime UploadedAt { get; set; }

        public string UploadedBy { get; set; }
    }
}