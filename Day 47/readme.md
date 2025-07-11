## Creation of Azure Storage Account

https://portal.azure.com/#home

![alt text](image.png)

- Click more services and storage accounts

![alt text](image-1.png)

-  Click Create

![alt text](image-2.png)

![alt text](image-3.png)

![alt text](image-4.png)

![alt text](image-5.png)

![alt text](image-6.png)

![alt text](image-7.png)

![alt text](image-8.png)

![alt text](image-9.png)

- Click Review and Create

![alt text](image-10.png)

- Click Create

![alt text](image-11.png)

## Creation of Containers

https://learn.microsoft.com/en-us/azure/storage/blobs/blob-containers-portal

- Click on rohithnewblob Storage

![alt text](image-14.png)

![alt text](image-15.png)

- Click Containers

![alt text](image-16.png)

- Click Add Container

![alt text](image-17.png)

- Click Create

![alt text](image-18.png)


- CLick on Container

![alt text](image-19.png)

- Click on Upload

![alt text](image-20.png)

![alt text](image-21.png)

- Click Upload

![alt text](image-22.png)

![alt text](image-23.png)

- Click on uploaded image name

![alt text](image-24.png)

- Copy the url
https://rohithnewblob.blob.core.windows.net/container1/image-1.png

- open in new tab

![alt text](image-25.png)

- Its visible now

## Generation of SAS tokens and Url

- In the container name you created, click on Shared Access Tokens

![alt text](image-26.png)

![alt text](image-27.png)

- Click on Generate SAS tokens and Url

![alt text](image-28.png)

## Creation of Dotnet WebAPI BlobAPI

```
dotnet new webapi -n BlobAPI
dotnet add package Swashbuckle.AspNetCore
dotnet add package Azure.Storage.Blobs
```

### Program.cs

```
using BlobAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<BlobStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
```

### Services/ BlobStorageService.cs

```
using Azure.Storage.Blobs;

namespace BlobAPI.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClinet;
        public BlobStorageService(IConfiguration configuration)
        {
            var sasUrl = configuration["AzureBlob:ContainerSasUrl"];
            _containerClinet = new BlobContainerClient(new Uri(sasUrl));
        }

        public async Task UploadFile(Stream fileStream,string fileName)
        {
            var blobClient = _containerClinet.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream,overwrite:true);
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
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

### Models/UploadRequestDto.cs

```
namespace BlobAPI.Models
{
    public class UploadRequestDto
    {
        public IFormFile File { get; set; }
    }
}
```

### Controllers/FilesController

```
using BlobAPI.Models;
using BlobAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly BlobStorageService _blobStorageService;

        public FilesController(BlobStorageService blobStorageService)
        {
            _blobStorageService  = blobStorageService;
        }
        [HttpGet]
        public async Task<IActionResult> Download(string fileName)
        {
            var stream = await _blobStorageService.DownloadFile(fileName);
            if (stream == null) 
                return NotFound();
            return File(stream, "application/octet-stream", fileName);
        }

        [Consumes("multipart/form-data")]
        
        [HttpPost("upload")]

        public async Task<IActionResult> Upload([FromForm] UploadRequestDto request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No file to upload");
            using var stream = request.File.OpenReadStream();
            await _blobStorageService.UploadFile(stream, request.File.FileName);
            return Ok("File uploaded");
        }
    }
}
```

### appsettings.json

```
{
  "AzureBlob": {
    "ContainerSasUrl": "https://rohithnewblob.blob.core.windows.net/container1?sp=racwdl&st=2025-07-11T06:00:14Z&se=2025-07-11T14:00:14Z&spr=https&sv=2024-11-04&sr=c&sig=6NTwTHiNxoHf1ClUD3MyUQTL391PuesJtu6r2OSonYQ%3D"
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

- Run the Application

Visit: http://localhost:5189/swagger/index.html

![alt text](image-29.png)

![alt text](image-31.png)

![alt text](image-30.png)

![alt text](image-32.png)

- Click Download File

![alt text](image-33.png)

![alt text](image-34.png)

![alt text](image-35.png)

![alt text](image-36.png)

## Static Website Hosting in Azure Storage

https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blob-static-website

- Go to your storage blob and click on Static Website in left side panel

![alt text](image-37.png)

- Enable it and add index.html, errror.html

![alt text](image-38.png)

- Cick Save

![alt text](image-39.png)

- Go to containers, $web will be visible

![alt text](image-40.png)

- Click $web, uplpoad your index.html file

### index.html

```
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Static Website</title>
</head>
<body>
    <h1> Hello Everyone! </h1>
    <h2>This is the static website hosted using Azure</h2>
</body>
</html>
```

- Click Upload

![alt text](image-41.png)

![alt text](image-42.png)

- Click Change Access Level and change it to Container

![alt text](image-43.png)

![alt text](image-44.png)

- Click index.html

![alt text](image-45.png)

- Copy paste the url in new tab

![alt text](image-46.png)


## Uploading Angular WareHouseArchiverFrontend to Azure

### Build the angular project

![alt text](image-47.png)

```
cd WareHouseFileArchiverFrontend
npm install
ng build --configuration production
```

- This will generate a dist/ folder

![alt text](image-48.png)


### Enable Static Website Hosting on Azure Storage


1. Go to your Storage Account in the Azure Portal.

2. Under "Data management", click "Static website".

3. Set Static website to Enabled.

4. Set:

```
Index document name: index.html

Error document path: index.html (for Angular routing)
```

5. Click Save.

![alt text](image-49.png)

![alt text](image-50.png)

### Upload Angular dist/ Files

1. Go to your Storage Account -> Data Storage → Containers → $web

2. Click Upload

3. Choose all files inside dist/your-app-name/browser (not the folder itself)

4. Make sure to select overwrite if files exist

5. Upload

![alt text](image-53.png)

![alt text](image-54.png)

- Copy the url in new tab to look the static website

![alt text](image-55.png)



## Use of blob storage for the project files and logs

### Use --auth-mode key (Without Needing RBAC)

1. Get the Storage Account Key

```
az storage account keys list \
  --account-name rohithnewblob \
  --resource-group Rohith_GenSparkTraining
```

![alt text](image-61.png)

```
[
  {
    "creationTime": "2025-07-11T04:37:08.079679+00:00",
    "keyName": "key1",
    "permissions": "FULL",
    "value": ""
  },
  {
    "creationTime": "2025-07-11T04:37:08.079679+00:00",
    "keyName": "key2",
    "permissions": "FULL",
    "value": ""
  }
]
```

![alt text](image-60.png)

### 1. In the backend code,

https://github.com/chriswill/serilog-sinks-azureblobstorage

- Install Required NuGet Package

```
dotnet add package Serilog.Sinks.AzureBlobStorage
dotnet add package Azure.Storage.Blobs
```

### Services/BlobUploaderService.cs

```
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace WareHouseFileArchiver.Services;
public class BlobUploaderService
{
    private readonly ILogger<BlobUploaderService> _logger;
    private readonly string _projectRoot = Directory.GetCurrentDirectory();
    private readonly string _logsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
    private const string ConnectionString = "";

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
```

### Modify Program.cs

```
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using WareHouseFileArchiver.Data;
using WareHouseFileArchiver.Interfaces;
using WareHouseFileArchiver.Models.Domains;
using WareHouseFileArchiver.Repositories;
using WareHouseFileArchiver.SignalRHub;
using WareHouseFileArchiver.Services; 
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// Serilog Configuration
// ----------------------------------------------------
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console()
    .WriteTo.File("Logs/WareHouseFileArchive_log.txt", rollingInterval: RollingInterval.Minute)
    .WriteTo.AzureBlobStorage(connectionString:"", storageContainerName:"project-logs")

    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// ----------------------------------------------------
// Services
// ----------------------------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WareHouseArchive", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<WareHouseArchiveAuthDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("WareHouseAuthConnectionString"));
});

builder.Services.AddDbContext<WareHouseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("WareHouseFileArchiver")
    .AddEntityFrameworkStores<WareHouseArchiveAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IArchiveFileRepository, ArchiveFileRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// ----------------------------------------------------
// Register your BlobUploaderService
// ----------------------------------------------------
builder.Services.AddSingleton<BlobUploaderService>();

// ----------------------------------------------------
// Rate Limiting
// ----------------------------------------------------
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("PerUserPolicy", context =>
    {
        var username = context.User.Identity?.Name ?? context.Request.Headers["X-UserId"].FirstOrDefault() ?? "anonymous";

        return RateLimitPartition.GetTokenBucketLimiter(username, _ => new TokenBucketRateLimiterOptions
        {
            TokenLimit = 1000,
            TokensPerPeriod = 10,
            ReplenishmentPeriod = TimeSpan.FromHours(1),
            AutoReplenishment = true,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });
    });
});

var app = builder.Build();

// ----------------------------------------------------
// Upload Project + Logs to Azure Blob Storage on Startup
// ----------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var uploader = scope.ServiceProvider.GetRequiredService<BlobUploaderService>();
    await uploader.UploadProjectFilesAsync();
}

// ----------------------------------------------------
// Middleware Pipeline
// ----------------------------------------------------
app.UseCors("AllowAll");
app.MapHub<NotificationsHub>("/notificationhub");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseRateLimiter();

app.MapControllers().RequireRateLimiting("PerUserPolicy");
app.Run();
```


### Output screenshots

- Initially, logs are empty

![alt text](image-62.png)

![alt text](image-63.png)

```
dotnet run
```
![alt text](image-64.png)

- 6 Log files

![alt text](image-65.png)

- Visit:

http://localhost:5239/swagger/index.html

![alt text](image-66.png)

![alt text](image-67.png)

- 7 Log files

![alt text](image-68.png)

- project-files and project-logs uploaded

![alt text](image-69.png)

![alt text](image-70.png)

![alt text](image-71.png)

![alt text](image-72.png)

- Download log.txt

![alt text](image-73.png)

- Shows log in endpoint done recently

![alt text](image-74.png)

![alt text](image-75.png)

- Download the file

![alt text](image-76.png)

- 8 Log files in local dir

![alt text](image-77.png)

- Refresh and download recent log.txt

![alt text](image-78.png)

- Shows recently downloaded CovidPolicy2023 File in container log.txt

![alt text](image-79.png)

