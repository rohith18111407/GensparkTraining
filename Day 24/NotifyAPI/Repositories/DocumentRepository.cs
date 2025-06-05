using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NotifyAPI.Data;
using NotifyAPI.Hubs;
using NotifyAPI.Interfaces;
using NotifyAPI.Models.Domains;

namespace NotifyAPI.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly NotifyAPIAuthDbContext dbContext;
        private readonly IHubContext<NotificationHub> hubContext;

        public DocumentRepository(NotifyAPIAuthDbContext dbContext, IHubContext<NotificationHub> hubContext)
        {
            this.dbContext = dbContext;
            this.hubContext = hubContext;
        }

        public async Task UploadAsync(IFormFile file, ClaimsPrincipal user)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var uploadedBy = user.FindFirst(ClaimTypes.Email)?.Value
                  ?? user.FindFirst("email")?.Value
                  ?? user.Identity?.Name
                  ?? "Unknown";

            var doc = new Document
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                Content = ms.ToArray(),
                UploadedAt = DateTime.UtcNow,
                UploadedBy = user.Identity?.Name ?? "Unknown"
            };

            await dbContext.Documents.AddAsync(doc);
            await dbContext.SaveChangesAsync();
            
            // Notify all clients
            var message = $"New document uploaded: {doc.FileName} by {doc.UploadedBy}";
            await hubContext.Clients.All.SendAsync("DocumentAdded", message);
        }

        public async Task<List<Document>> GetAllAsync()
        {
            return await dbContext.Documents.ToListAsync();
        }
    }
}