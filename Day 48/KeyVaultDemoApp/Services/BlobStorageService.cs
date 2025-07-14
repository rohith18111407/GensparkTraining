using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;

namespace KeyVaultDemoApp.Services;

public class BlobStorageService
{
    private BlobContainerClient _containerClient;
    private readonly IConfiguration _config;

    public BlobStorageService(IConfiguration config)
    {
        _config = config;
    }

    private async Task InitClient()
    {
        if (_containerClient != null)
            return;

        var keyVaultUrl = _config["AzureBlob:KeyVaultUrl"];
        var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

        var secret = await secretClient.GetSecretAsync("MyNewBlobContainerSas"); 
        var sasUrl = secret.Value.Value;

        _containerClient = new BlobContainerClient(new Uri(sasUrl));
    }

    public async Task UploadAsync(Stream fileStream, string fileName)
    {
        await InitClient();
        var blobClient = _containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true);
    }

    public async Task<Stream?> DownloadAsync(string fileName)
    {
        await InitClient();
        var blobClient = _containerClient.GetBlobClient(fileName);

        if (await blobClient.ExistsAsync())
        {
            var download = await blobClient.DownloadStreamingAsync();
            return download.Value.Content;
        }

        return null;
    }
}
