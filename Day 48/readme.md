## Azure Key Vaults

https://portal.azure.com/#home

- Search for Key Vaults

![alt text](image.png)

- Click Create

![alt text](image-1.png)

![alt text](image-2.png)

![alt text](image-3.png)

- Click Next

![alt text](image-4.png)

- Choose Vault Access Policy

- Click Next

![alt text](image-5.png)

- Click Review+Create

![alt text](image-6.png)

![alt text](image-7.png)

- Go to your Key Vault, Click on Generate/Import Secrets

![alt text](image-8.png)

- under BlobAPI, in appsettings.json, ContainerSasUrl is the secret value in Secrets of Key Vaults 

```
 "AzureBlob": {
    "ContainerSasUrl": "https://rohithnewblob.blob.core.windows.net/container1?sp=racwdl&st=2025-07-11T06:00:14Z&se=2025-07-11T14:00:14Z&spr=https&sv=2024-11-04&sr=c&sig=6NTwTHiNxoHf1ClUD3MyUQTL391PuesJtu6r2OSonYQ%3D"
  },
```

- Click Create

![alt text](image-9.png)

![alt text](image-10.png)

- Install the following packages

```
dotnet add package Azure.Identity
dotnet add package Azure.Security.KeyVault.Secrets
```

- Go to Overview and copy the Vault Uri

![alt text](image-11.png)

### Modify appsettings.json

```
{
  "AzureBlob": {
    "ContainerSasUrl": "https://rohithnewblob.blob.core.windows.net/container1?sp=racwdl&st=2025-07-11T06:00:14Z&se=2025-07-11T14:00:14Z&spr=https&sv=2024-11-04&sr=c&sig=6NTwTHiNxoHf1ClUD3MyUQTL391PuesJtu6r2OSonYQ%3D",
    "KeyVaultUrl": "https://rohith-key.vault.azure.net/"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```


### Modify BlobStorageService.cs

```

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;

namespace BlobAPI.Services
{
    public class BlobStorageService
    {
        private  BlobContainerClient _containerClinet;
        private readonly IConfiguration _configuration;

        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        private async Task UpdateContainerClient()
        {
            var blobUrl = _configuration["AzureBlob:KeyVaultUrl"];
            SecretClient secretClient = new SecretClient(new Uri(blobUrl), new DefaultAzureCredential());
            KeyVaultSecret secret = await secretClient.GetSecretAsync("SasUrl");
            var blobUrlValue = secret.Value;
            _containerClinet = new BlobContainerClient(new Uri(blobUrlValue));
        }

        public async Task UploadFile(Stream fileStream,string fileName)
        {
            await UpdateContainerClient();
            var blobClient = _containerClinet.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream,overwrite:true);
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            await UpdateContainerClient();
            var blobClient = _containerClinet?.GetBlobClient(fileName);
            if(await blobClient.ExistsAsync())
            {
                var downloadInfor = await blobClient.DownloadStreamingAsync();
                return downloadInfor.Value.Content;
            }
            return null;
        }
    }
}
```

- Go to your Blob Container and generate SAS token and URl as it might get expired

- Copy Blob SAS Url

![alt text](image-21.png)

- Use it in ContainerSasUrl1 in appsettings.json, also paste value in Secret Value

![alt text](image-23.png)

![alt text](image-24.png)

![alt text](image-25.png)

- Copy VaultUri and paste it in KeyVaultUrl under appsettings.json

### Modiy appsettings.json

```
{
  "AzureBlob": {
    "ContainerSasUrl1": "https://rohithnewblob.blob.core.windows.net/container1?sp=racwdl&st=2025-07-14T05:26:12Z&se=2025-07-14T13:26:12Z&sv=2024-11-04&sr=c&sig=y6tl8WRe1ObuKoozAwqQz1M7JFyzPFUearF%2B3YY82Bk%3D",
    "KeyVaultUrl": "https://rohith-key.vault.azure.net/"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Modify BlobStorageService.cs


```

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;

namespace BlobAPI.Services
{
    public class BlobStorageService
    {
        private  BlobContainerClient _containerClinet;
        private readonly IConfiguration _configuration;

        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        private async Task UpdateContainerClient()
        {
            var blobUrl = _configuration["AzureBlob:KeyVaultUrl"];
            SecretClient secretClient = new SecretClient(new Uri(blobUrl), new DefaultAzureCredential());
            KeyVaultSecret secret = await secretClient.GetSecretAsync("ContainerSasUrl1");
            var blobUrlValue = secret.Value;
            _containerClinet = new BlobContainerClient(new Uri(blobUrlValue));
        }

        public async Task UploadFile(Stream fileStream,string fileName)
        {
            await UpdateContainerClient();
            var blobClient = _containerClinet.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream,overwrite:true);
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            await UpdateContainerClient();
            var blobClient = _containerClinet?.GetBlobClient(fileName);
            if(await blobClient.ExistsAsync())
            {
                var downloadInfor = await blobClient.DownloadStreamingAsync();
                return downloadInfor.Value.Content;
            }
            return null;
        }
    }
}
```

### terminal

```
az login
```
![alt text](image-12.png)

```
dotnet run
```

- Visit: http://localhost:5189/swagger/index.html

![alt text](image-26.png)

![alt text](image-27.png)



## KeyVaultDemoApp

- Create a container named newcontainer under rohithnewblob and generate SAS token and Url

![alt text](image-28.png)

- Add a secret named MyNewBlobContainerSas under rohith-key

![alt text](image-29.png)

```
New Key Vault URL	https://rohith-key.vault.azure.net/
Secret Name	= MyNewBlobContainerSas
Secret Value	https://rohithnewblob.blob.core.windows.net/newcontainer?sp=racwdl&st=2025-07-14T10:34:57Z&se=2025-07-14T18:34:57Z&sv=2024-11-04&sr=c&sig=%2BAwEmcMIYX5ou5IMcZkbpwleJTgX9xCKE%2Bo3kXTu%2FGM%3D
Storage Container	newcontainer (already created with correct SAS permissions)
```

![alt text](image-30.png)

### appsettings.json

```
{
  "AzureBlob": {
    "KeyVaultUrl": "https://rohith-key.vault.azure.net/"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Models/DTOs/UploadFileDto.cs

```
using Microsoft.AspNetCore.Http;

namespace KeyVaultDemoApp.Models.DTOs;

public class UploadFileDto
{
    public IFormFile File { get; set; }
}
```

### Controllers/FileController.cs

```
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
```

### Services/BlobStorageService.cs

```
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
```

### Program.cs

```
using KeyVaultDemoApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddSingleton<BlobStorageService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

### terminal

```
dotnet run
```

- Visit: http://localhost:5064/swagger/index.html

![alt text](image-31.png)

![alt text](image-32.png)

![alt text](image-33.png)

![alt text](image-34.png)


