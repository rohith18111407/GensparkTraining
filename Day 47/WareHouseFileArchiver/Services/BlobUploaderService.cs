using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace WareHouseFileArchiver.Services;
public class BlobUploaderService
{
    private readonly ILogger<BlobUploaderService> _logger;
    private readonly string _projectRoot = Directory.GetCurrentDirectory();
    private readonly string _logsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
    private const string ConnectionString = AzureBlobStorageConnectionString;

    public BlobUploaderService(ILogger<BlobUploaderService> logger)
    {
        _logger = logger;
    }

    public async Task UploadProjectFilesAsync()
    {
        var container = new BlobContainerClient(ConnectionString, "project-files");
        await container.CreateIfNotExistsAsync();

        var files = Directory.GetFiles(_projectRoot, "*", SearchOption.AllDirectories)
                             .Where(f => !f.Contains("Logs")); // exclude log files

        foreach (var filePath in files)
        {
            string relativePath = Path.GetRelativePath(_projectRoot, filePath).Replace("\\", "/");
            var blobClient = container.GetBlobClient(relativePath);
            using var stream = File.OpenRead(filePath);
            await blobClient.UploadAsync(stream, overwrite: true);
            _logger.LogInformation($"Uploaded project file: {relativePath}");
        }
    }
}
