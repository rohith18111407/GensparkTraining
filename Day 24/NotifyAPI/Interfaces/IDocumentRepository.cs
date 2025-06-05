using System.Security.Claims;
using NotifyAPI.Models.Domains;

namespace NotifyAPI.Interfaces
{
    public interface IDocumentRepository
    {
        Task UploadAsync(IFormFile file, ClaimsPrincipal user);
        Task<List<Document>> GetAllAsync();
    }
}