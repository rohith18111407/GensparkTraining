using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WareHouseFileArchiver.Interfaces;
using WareHouseFileArchiver.Models.Domains;
using WareHouseFileArchiver.Models.DTOs;
using WareHouseFileArchiver.SignalRHub;

namespace WareHouseFileArchiver.Controllers
{
    [ApiController]
    [Route("api/v1/files")]
    public class FilesController : ControllerBase
    {
        private readonly IArchiveFileRepository archiveFileRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<NotificationsHub> hubContext;

        public FilesController(IArchiveFileRepository archiveFileRepository,
                               IWebHostEnvironment webHostEnvironment,
                               IHttpContextAccessor httpContextAccessor,
                               UserManager<ApplicationUser> userManager,
                               IHubContext<NotificationsHub> hubContext)
        {
            this.archiveFileRepository = archiveFileRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.hubContext = hubContext;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadArchiveFileRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Validation failed",
                    data = (object?)null,
                    errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray())
                });
            }

            var itemExists = await archiveFileRepository.ItemExistsAsync(request.ItemId);
            if (!itemExists)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Item with ID {request.ItemId} does not exist.",
                    data = (object?)null,
                    errors = new { ItemId = new[] { "Invalid ItemId." } }
                });
            }

            var item = await archiveFileRepository.GetItemByIdAsync(request.ItemId);
            // if (!Enum.TryParse<CategoryType>(item.Category, out var itemCategoryEnum) || itemCategoryEnum != request.Category)
            // {
            //     return BadRequest(new
            //     {
            //         success = false,
            //         message = $"File category '{request.Category}' does not match item category '{item.Category}'",
            //         data = (object?)null,
            //         errors = new { Category = new[] { "Category mismatch or invalid category string." } }
            //     });
            // }

            // Get user who is uploading the file
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            var uploadedBy = user?.UserName ?? "Unknown";

            // Check for existing file with the same name and item
            var existingFile = await archiveFileRepository.GetLatestVersionAsync(
                Path.GetFileNameWithoutExtension(request.File.FileName),
                request.ItemId
            );

            int newVersion = existingFile != null ? existingFile.VersionNumber + 1 : 1;

            // Mark the existing file as updated
            if (existingFile != null)
            {
                existingFile.UpdatedAt = DateTime.UtcNow;
                existingFile.UpdatedBy = uploadedBy;
                await archiveFileRepository.UpdateAsync(existingFile);
            }

            var fileExtension = Path.GetExtension(request.File.FileName);
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(request.File.FileName)}_{DateTime.UtcNow.Ticks}{fileExtension}";
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "ArchiveFiles", uniqueFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            using var stream = new FileStream(filePath, FileMode.Create);
            await request.File.CopyToAsync(stream);

            var archiveFile = new ArchiveFile
            {
                Id = Guid.NewGuid(),
                ItemId = request.ItemId,
                FileName = Path.GetFileNameWithoutExtension(request.File.FileName),
                FileExtension = fileExtension,
                FileSizeInBytes = request.File.Length,
                FilePath = filePath,
                CreatedBy = uploadedBy,
                CreatedAt = DateTime.UtcNow,
                Description = request.Description,
                Category = request.Category,
                VersionNumber = newVersion
            };

            await archiveFileRepository.UploadAsync(archiveFile);

            // Notify clients
            await hubContext.Clients.All.SendAsync("ReceiveNotification", new
            {
                Action = existingFile != null ? "File Version Updated" : "New File Uploaded",
                archiveFile.Id,
                archiveFile.FileName,
                archiveFile.Category,
                archiveFile.FileExtension,
                archiveFile.FileSizeInBytes,
                archiveFile.FilePath,
                archiveFile.VersionNumber,
                archiveFile.ItemId,
                ItemName = item?.Name,
                archiveFile.CreatedAt,
                archiveFile.CreatedBy,
                archiveFile.UpdatedAt,
                archiveFile.UpdatedBy
            });
            
            return Ok(new
            {
                success = true,
                message = existingFile != null ? "File version updated successfully" : "File uploaded successfully",
                data = new ArchiveFileResponseDto
                {
                    Id = archiveFile.Id,
                    FileName = archiveFile.FileName,
                    Category = archiveFile.Category,
                    FileExtension = archiveFile.FileExtension,
                    FileSizeInBytes = archiveFile.FileSizeInBytes,
                    FilePath = archiveFile.FilePath,
                    VersionNumber = archiveFile.VersionNumber,
                    Description = archiveFile.Description,
                    ItemId = request.ItemId,
                    ItemName = archiveFile.Item?.Name,
                    CreatedAt = archiveFile.CreatedAt,
                    CreatedBy = archiveFile.CreatedBy
                },
                errors = (object?)null
            });
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("{filename}/v{version}")]
        public async Task<IActionResult> Download(string filename, int version)
        {
            var archiveFile = await archiveFileRepository.GetFileByNameAndVersionAsync(filename, version);
            // if (archiveFile == null) return NotFound();

            // if (!System.IO.File.Exists(archiveFile.FilePath))
            // {
            //     return NotFound("File not found on disk.");
            // }

            if (archiveFile == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "File not found",
                    data = (object?)null,
                    errors = new { File = new[] { "No file with that name and version." } }
                });
            }

            if (!System.IO.File.Exists(archiveFile.FilePath))
            {
                return NotFound(new
                {
                    success = false,
                    message = "File not found on disk",
                    data = (object?)null,
                    errors = new { File = new[] { "Physical file not found." } }
                });
            }

            await hubContext.Clients.All.SendAsync("ReceiveNotification", new
            {
                Action = "File Downloaded",
                archiveFile.Id,
                archiveFile.FileName,
                archiveFile.Category,
                archiveFile.FileExtension,
                archiveFile.FileSizeInBytes,
                archiveFile.FilePath,
                archiveFile.VersionNumber,
                archiveFile.ItemId,
                ItemName = archiveFile.Item?.Name,
                archiveFile.CreatedAt,
                archiveFile.CreatedBy,
                archiveFile.UpdatedAt,
                archiveFile.UpdatedBy
            });


            var stream = new FileStream(archiveFile.FilePath, FileMode.Open, FileAccess.Read);
            return File(stream, "application/octet-stream", $"{archiveFile.FileName}{archiveFile.FileExtension}");
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("by-item/{itemId}")]
        public async Task<IActionResult> GetByItemId(Guid itemId)
        {
            var files = await archiveFileRepository.GetFilesByItemIdAsync(itemId);
            // return Ok(files);
            return Ok(new
            {
                success = true,
                message = "Files fetched successfully",
                data = files,
                errors = (object?)null
            });
        }
    }
}