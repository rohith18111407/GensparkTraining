# Azure App Services, Web App Deployment

## Create an App Service

![alt text](image-14.png)

- Click Create Web App

![alt text](image-15.png)

![alt text](image-17.png)

![alt text](image-21.png)

- Click Create

![alt text](image-19.png)

![alt text](image-20.png)

![alt text](image-22.png)

![alt text](image-23.png)


## Install Azure App Service in VS Code Extensions

```
dotnet run
```

Visit: http://localhost:5134/swagger/index.html

- Click on Azure in left side of VS code then select your app service

![alt text](image-24.png)

- Click Deploy to Web App

![alt text](image-25.png)

- Select BlobAPI and click deploy

![alt text](image-26.png)


- Now Click on Default Domain

![alt text](image-27.png)

Visit: rohithblobapi-fegchrb2bwhzgxcx.eastus-01.azurewebsites.net




---------------------

xxxxx



## Creation of New Azure Registry

- Click Create Registry

![alt text](image.png)

- Press Enter

![alt text](image-1.png)

- Select Premium

![alt text](image-2.png)

- Click Create New Resource Group

![alt text](image-3.png)

- Select East US location

![alt text](image-4.png)

- Successfully created Resource Group and Azure Container Registry

![alt text](image-5.png)


xxxxxx

---------------------------------


# Azure Sample App Deployment using Azure Container Registry

xxxxxx

## create a New Repository

![alt text](image-6.png)

- Copy the HTTPS

![alt text](image-7.png)

- Click Cmd+Shift+P to open cmd palette and search for add docker

![alt text](image-8.png)

![alt text](image-9.png)

- Build Image in Azure

![alt text](image-10.png)

- Type latest tag

![alt text](image-11.png)

- Click Azure and then select your acr

![alt text](image-12.png)

- Select Linux

![alt text](image-13.png)

- Image gets published in ACR

xxxxxx
------------------------




## terminal

```
az login
```

![alt text](image-29.png)

## 1. Build your .NET app

```
dotnet publish -c Release -o ./publish
```

![alt text](image-30.png)


##  2. Create Azure resources

### 2.1 Create Resource Group (if needed):

```
az group create --name rohith-Resource-Group --location eastus
```

![alt text](image-31.png)

###  2.2 Create Azure Container Registry

```
az acr create --resource-group rohith-Resource-Group \
  --name rohithnewacr20250721 \
  --sku Basic \
  --admin-enabled true
```

![alt text](image-32.png)

![alt text](image-33.png)

### 2.3 Get the login server URL

```
az acr show --name rohithnewacr20250721 --query "loginServer" --output tsv
```

![alt text](image-34.png)

```
rohithnewacr20250721.azurecr.io
```
### 3. Log in to ACR locally

```
az acr login --name rohithnewacr20250721
```

### 4. Dockerfile

```
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish "SampleAppForDeployment.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SampleAppForDeployment.dll"]
```


### 5. Build Docker image

```
docker build -t rohithnewacr20250721.azurecr.io/sampleappfordeployment:latest .
```

![alt text](image-35.png)

### 6. Push Docker image to ACR

```
docker push rohithnewacr20250721.azurecr.io/sampleappfordeployment:latest
```

![alt text](image-36.png)

### 7. Verify image in ACR

```
az acr repository list --name rohithnewacr20250721 --output table
```

![alt text](image-37.png)

## Azure Portal

![alt text](image-39.png)

![alt text](image-40.png)

![alt text](image-41.png)

![alt text](image-42.png)


# Question.txt

```
Objective
Develop a Training Video Portal where:
 
Users can upload training videos (e.g., company induction videos).
 
Users can browse and stream videos directly in the Angular app.
 
Video metadata is stored in Azure SQL/PostgreSQL.
 
Videos themselves are stored in Azure Blob Storage and streamed via public or SAS URLs.
 
Scope Overview
Backend (.NET 8 Web API)
Endpoints:
 
POST /api/videos/upload
 
Accepts video file + metadata (Title, Description).
 
Saves video to Blob Storage.
 
Saves metadata and blob URL to the DB.
 
GET /api/videos
 
Returns a list of videos (Title, Description, URL).
 
GET /api/videos/{id}/stream
 
(Optional) Returns a streaming response from Blob Storage.
 
Or, the Angular app can stream directly via Blob URL.
 
Database Table:
 
CREATE TABLE TrainingVideos (
  Id INT IDENTITY PRIMARY KEY,
  Title NVARCHAR(200),
  Description NVARCHAR(500),
  UploadDate DATETIME,
  BlobUrl NVARCHAR(500)
);
Frontend (Angular)
Video List Page
 
Shows:
 
Title
 
Description
 
Video player embedded (HTML5 <video> tag).
 
User can click a video to stream.
 
Upload Page
 
Form to upload:
 
Title
 
Description
 
Video File
 
Functional Requirements
User can upload videos to Blob Storage.
 
Video metadata saved to DB.
 
User can stream videos from Blob Storage.
 
Frontend shows embedded video players.
 
Azure Blob Storage
Container: training-videos
 
Upload files (MP4 recommended).
 
Use either:
 
Public container (for simpler streaming), or
 
Generate SAS tokens for secured access.
 
Tech Requirements
.NET 8 Web API
 
Angular 17
 
Azure SQL/PostgreSQL
 
Azure Blob Storage SDK
 
Swagger for API testing
 
Estimated Time Allocation (~5 Hours)
Task	Estimated Time
API project scaffolding, DB migration, blob config	45 min
Upload endpoint + Blob Storage integration	45 min
List endpoint	20 min
Streaming endpoint (or SAS URL logic)	30 min
Angular project scaffold	30 min
Video List page with <video> players	45 min
Upload form with file picker	45 min
Buffer / Debug	20 min
 
Deliverables
Participants should submit:
 
Backend repo with:
 
Upload and list endpoints.
 
Azure Blob Storage integration.
 
Angular repo:
 
Video list displaying embedded players.
 
Upload form.
 
Screenshots showing:
 
Videos stored in Blob Storage.
 
Videos successfully streaming in the app.
```

## Creation of Azure Blob Storage

https://portal.azure.com/#home

- Click on Storage Accounts

![alt text](image-43.png)

- Click Create

![alt text](image-44.png)

- Click Review + Create

![alt text](image-45.png)

- Click Create

![alt text](image-46.png)

![alt text](image-47.png)

![alt text](image-48.png)

![alt text](image-49.png)

## Obtaining Storage Account Connection String

```
1. Go to your Azure Portal → Storage Account.

2. Navigate to: Access Keys under "Security + networking".

3. Copy the Connection String.
```

![alt text](image-57.png)

![alt text](image-58.png)

- Copy one of the connection string

```
DefaultEndpointsProtocol=https;AccountName=rohithtrainingstorage;AccountKey=4KGSQyreAdb0p0b3Vsn+p+vPJbpwQ8Id+IdVgD7i0141kgNKAOdz4QoBkC1OtvvvfoHBday9HINY+AStuhfowA==;EndpointSuffix=core.windows.net
```

## Creation of TrainingVideoPortalAPI

### TrainingVideoDbContext.cs

```
using Microsoft.EntityFrameworkCore;
using TrainingVideoPortalAPI.Models;

namespace TrainingVideoPortalAPI.Contexts
{
    public class TrainingVideoDbContext : DbContext
    {
        public TrainingVideoDbContext(DbContextOptions<TrainingVideoDbContext> options)
            : base(options) { }

        public DbSet<TrainingVideo> TrainingVideos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainingVideo>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }

    }
}
```

### TrainingVideosController.cs

```
using Microsoft.AspNetCore.Mvc;
using TrainingVideoPortalAPI.Models.DTOs;
using TrainingVideoPortalAPI.Models;
using TrainingVideoPortalAPI.Services;

namespace TrainingVideoPortalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingVideosController : ControllerBase
    {
        private readonly ITrainingVideoService _videoService;

        public TrainingVideosController(ITrainingVideoService videoService)
        {
            _videoService = videoService;
        }

        [Consumes("multipart/form-data")]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo([FromForm] TrainingVideoUploadRequestDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Video file is required.");

            var video = await _videoService.UploadVideoAsync(dto);
            return CreatedAtAction(nameof(GetVideoById), new { id = video.Id }, video);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingVideo>>> GetAllVideos()
        {
            var videos = await _videoService.GetAllVideosAsync();
            return Ok(videos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingVideo>> GetVideoById(Guid id)
        {
            var video = await _videoService.GetVideoByIdAsync(id);
            if (video == null)
                return NotFound();

            return Ok(video);
        }
    }
}
```

### IRepository.cs

```
namespace TrainingVideoPortalAPI.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        public Task<T> Add(T item);
        public Task<T> Get(K key);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Update(K key, T item);
        public Task<T> Delete(K key);
    }
}
```

### ITrainingVideoService.cs

```
using TrainingVideoPortalAPI.Models;
using TrainingVideoPortalAPI.Models.DTOs;

namespace TrainingVideoPortalAPI.Services
{
    public interface ITrainingVideoService
    {
        Task<TrainingVideoResponseDto> UploadVideoAsync(TrainingVideoUploadRequestDto dto);
        Task<IEnumerable<TrainingVideoResponseDto>> GetAllVideosAsync();
        Task<TrainingVideoResponseDto?> GetVideoByIdAsync(Guid id);
    }

}
```

### TrainingVideoResponseDto.cs

```
namespace TrainingVideoPortalAPI.Models.DTOs
{
    public class TrainingVideoResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string BlobUrl { get; set; } = null!;
        public DateTime UploadDate { get; set; }
    }
}
```

### TrainingCVideoUploadRequestDto.cs

```
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
```

### TrainingVideo.cs

```
namespace TrainingVideoPortalAPI.Models
{
    public class TrainingVideo
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime UploadDate { get; set; }
        public string BlobUrl { get; set; } = null!;
    }
}
```

### Repository.cs

```
using TrainingVideoPortalAPI.Contexts;
using TrainingVideoPortalAPI.Interfaces;

namespace TrainingVideoPortalAPI.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly TrainingVideoDbContext _context;

        public Repository(TrainingVideoDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot add null entity.");

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(K key)
        {
            var item = await Get(key);
            if (item == null)
                throw new KeyNotFoundException("Entity not found for deletion.");

            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> Update(K key, T item)
        {
            var existing = await Get(key);
            if (existing == null)
                throw new KeyNotFoundException("Entity not found for update.");

            _context.Entry(existing).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public abstract Task<T> Get(K key);
        public abstract Task<IEnumerable<T>> GetAll();
    }
}
```

### TrainingVideoRepository.cs

```
using Microsoft.EntityFrameworkCore;
using TrainingVideoPortalAPI.Contexts;
using TrainingVideoPortalAPI.Interfaces;
using TrainingVideoPortalAPI.Models;

namespace TrainingVideoPortalAPI.Repositories
{
    public class TrainingVideoRepository : Repository<Guid, TrainingVideo>
    {
        public TrainingVideoRepository(TrainingVideoDbContext context) : base(context) { }

        public override async Task<TrainingVideo> Get(Guid key)
        {
            return await _context.TrainingVideos.FindAsync(key)
                   ?? throw new KeyNotFoundException("Video not found");
        }

        public override async Task<IEnumerable<TrainingVideo>> GetAll()
        {
            return await _context.TrainingVideos
                .OrderByDescending(v => v.UploadDate)
                .ToListAsync();
        }
    }
}
```

### BlobStorageService.cs

```
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace TrainingVideoPortalAPI.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            var containerName = configuration["AzureBlobStorage:ContainerName"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            _containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty.");

            var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var blobClient = _containerClient.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return blobClient.Uri.ToString(); 
        }
    }
}
```

### TrainingVideoService.cs

```
using TrainingVideoPortalAPI.Contexts;
using TrainingVideoPortalAPI.Models;
using TrainingVideoPortalAPI.Interfaces;
using TrainingVideoPortalAPI.Models.DTOs;

namespace TrainingVideoPortalAPI.Services
{
    public class TrainingVideoService : ITrainingVideoService
    {
        private readonly IRepository<Guid, TrainingVideo> _repository;
        private readonly BlobStorageService _blobStorageService;

        public TrainingVideoService(
            IRepository<Guid, TrainingVideo> repository,
            BlobStorageService blobStorageService)
        {
            _repository = repository;
            _blobStorageService = blobStorageService;
        }

        public async Task<TrainingVideoResponseDto> UploadVideoAsync(TrainingVideoUploadRequestDto dto)
        {
            var blobUrl = await _blobStorageService.UploadFileAsync(dto.File);
            var video = new TrainingVideo
            {
                Title = dto.Title,
                Description = dto.Description,
                UploadDate = DateTime.UtcNow,
                BlobUrl = blobUrl
            };

            var savedVideo = await _repository.Add(video);
            return MapToDto(savedVideo);
        }

        public async Task<IEnumerable<TrainingVideoResponseDto>> GetAllVideosAsync()
        {
            var videos = await _repository.GetAll();
            return videos.Select(MapToDto);
        }

        public async Task<TrainingVideoResponseDto?> GetVideoByIdAsync(Guid id)
        {
            try
            {
                var video = await _repository.Get(id);
                return MapToDto(video);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
        
        private TrainingVideoResponseDto MapToDto(TrainingVideo video)
        {
            return new TrainingVideoResponseDto
            {
                Id = video.Id,
                Title = video.Title,
                Description = video.Description,
                UploadDate = video.UploadDate,
                BlobUrl = video.BlobUrl
            };
        }
    }
}
```

### Program.cs

```
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrainingVideoPortalAPI.Contexts;
using TrainingVideoPortalAPI.Interfaces;
using TrainingVideoPortalAPI.Models;
using TrainingVideoPortalAPI.Repositories;
using TrainingVideoPortalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opts.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddDbContext<TrainingVideoDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IRepository<Guid, TrainingVideo>, TrainingVideoRepository>();
builder.Services.AddTransient<ITrainingVideoService, TrainingVideoService>();
builder.Services.AddTransient<BlobStorageService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalhost4200");
app.MapControllers();

app.Run();
```

### appsettings.json

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "User ID=postgres;Password=ROHITH;Host=localhost;Port=5432;Database=TrainingVideoApiDB;"
  },
  "AzureBlobStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=rohithtrainingstorage;AccountKey=4KGSQyreAdb0p0b3Vsn+p+vPJbpwQ8Id+IdVgD7i0141kgNKAOdz4QoBkC1OtvvvfoHBday9HINY+AStuhfowA==;EndpointSuffix=core.windows.net",
    "ContainerName" : "training-videos"
  }
}
```

### Visit: http://localhost:5160/swagger/index.html

![alt text](image-69.png)

![alt text](image-70.png)

![alt text](image-67.png)

![alt text](image-68.png)

![alt text](image-71.png)

![alt text](image-72.png)

![alt text](image-73.png)

- Click Change Access Level

![alt text](image-74.png)

- Click OK

![alt text](image-75.png)

- Copy the URL

![alt text](image-76.png)

```
https://rohithtrainingstorage.blob.core.windows.net/training-videos/2d3e593e-59ce-4154-9c01-e9f7cb006b9d.mp4
```

![alt text](image-77.png)

## Creation of training-video-frontend

### app.html

```
<!-- <app-training-video-upload></app-training-video-upload> -->
<router-outlet></router-outlet>
```

### app.config.ts

```
import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient()
  ]
};
```

### training-video.service.ts

```
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TrainingVideo } from '../models/training-video';
import { TrainingVideoUpload } from '../models/training-video-upload';

@Injectable({
  providedIn: 'root',
})
export class TrainingVideoService {
  private readonly baseUrl = 'http://localhost:5160/api/TrainingVideos'; 

  private http = inject(HttpClient);

  getAllVideos(): Observable<TrainingVideo[]> {
    return this.http.get<TrainingVideo[]>(this.baseUrl);
  }

  getVideoById(id: string): Observable<TrainingVideo> {
    return this.http.get<TrainingVideo>(`${this.baseUrl}/${id}`);
  }

  uploadVideo(data: TrainingVideoUpload): Observable<TrainingVideo> {
    const formData = new FormData();
    formData.append('title', data.title);
    formData.append('description', data.description);
    formData.append('file', data.file);

    return this.http.post<TrainingVideo>(`${this.baseUrl}/upload`, formData);
 }


}
```

### training-video.ts

```
export interface TrainingVideo {
  id: string;
  title: string;
  description: string;
  uploadDate: string; 
  blobUrl: string;
}
```

### training-video-upload.ts

```
export interface TrainingVideoUpload {
  title: string;
  description: string;
  file: File;
}
```

### training-video-upload.ts

```
import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TrainingVideoUpload } from '../../models/training-video-upload';
import { TrainingVideoService } from '../../services/training-video.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-training-video-upload',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './training-video-upload.html',
  styleUrl: './training-video-upload.css'
})
export class TrainingVideoUploadComponent {
  model: Partial<TrainingVideoUpload> = {
    title: '',
    description: '',
  };

  selectedFile: File | null = null;
  isUploading = false;
  uploadSuccess = false;
  uploadError = false;
  showToast = false;

  private uploadService = inject(TrainingVideoService);
  private router = inject(Router);

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit() {
    if (!this.selectedFile) return;

    const payload: TrainingVideoUpload = {
      title: this.model.title!,
      description: this.model.description!,
      file: this.selectedFile,
    };

    this.isUploading = true;
    this.uploadService.uploadVideo(payload).subscribe({
      next: () => {
        this.uploadSuccess = true;
        this.uploadError = false;
        this.selectedFile = null;
        this.isUploading = false;
        this.showToast = true;
        this.model = { title: '', description: '' };
        setTimeout(() => {
          this.router.navigate(['/videos']);
        }, 2000);
      },
      error: () => {
        this.uploadSuccess = false;
        this.uploadError = true;
        this.isUploading = false;
      },
    });
  }
}
```

### training-video-upload.html

```
<div class="d-flex align-items-center justify-content-center min-vh-100 bg-light">
  <div class="card shadow-sm p-4" style="width: 100%; max-width: 400px;">
    <h2 class="text-center mb-4">Upload Training Video</h2>

    <form (ngSubmit)="onSubmit()" #uploadForm="ngForm">
      <div class="mb-3">
        <label for="title" class="form-label">Title</label>
        <input
          type="text"
          id="title"
          name="title"
          class="form-control"
          [(ngModel)]="model.title"
          required
          #titleRef="ngModel"
        />
        <div *ngIf="titleRef.invalid && titleRef.touched" class="text-danger small mt-1">
          Title is required.
        </div>
      </div>

      <div class="mb-3">
        <label for="description" class="form-label">Description</label>
        <textarea
          id="description"
          name="description"
          class="form-control"
          rows="3"
          [(ngModel)]="model.description"
          required
          #descRef="ngModel"
        ></textarea>
        <div *ngIf="descRef.invalid && descRef.touched" class="text-danger small mt-1">
          Description is required.
        </div>
      </div>

      <div class="mb-3">
        <label for="file" class="form-label">Video File</label>
        <input
          type="file"
          id="file"
          accept="video/*"
          class="form-control"
          (change)="onFileSelected($event)"
          required
        />
      </div>

      <button
        type="submit"
        class="btn btn-primary w-100"
        [disabled]="!uploadForm.form.valid || !selectedFile || isUploading"
      >
        <span *ngIf="!isUploading">Upload</span>
        <span *ngIf="isUploading">Uploading...</span>
      </button>

    </form>

    <div class="mt-3 text-center">
      <p *ngIf="uploadSuccess" class="text-success">Upload successful!</p>
      <p *ngIf="uploadError" class="text-danger">Upload failed. Try again.</p>
    </div>
  </div>
</div>

<div
  class="toast align-items-center text-bg-success border-0 position-fixed top-0 end-0 m-4"
  role="alert"
  [class.show]="showToast"
  [class.hide]="!showToast"
>
  <div class="d-flex">
    <div class="toast-body">
      Video uploaded successfully!
    </div>
    <button
      type="button"
      class="btn-close btn-close-white me-2 m-auto"
      (click)="showToast = false"
    ></button>
  </div>
</div>
```

### training-video-list.ts

```
import { Component, inject, OnInit } from '@angular/core';
import { TrainingVideo } from '../../models/training-video';
import { TrainingVideoService } from '../../services/training-video.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-training-video-list',
  imports: [CommonModule, RouterLink],
  templateUrl: './training-video-list.html',
  styleUrl: './training-video-list.css'
})
export class TrainingVideoListComponent implements OnInit {
  videos: TrainingVideo[] = [];

  private videoService = inject(TrainingVideoService);

  ngOnInit(): void {
    this.videoService.getAllVideos().subscribe({
      next: (res) => {
        this.videos = res;
      },
      error: (err) => console.error('Failed to load videos:', err)
    });
  }
}
```

### training-video-list.html

```
<div class="container py-4">
  <h2 class="text-center mb-4">Training Videos</h2>

  <div class="d-flex justify-content-end mb-4">
  <a class="btn btn-outline-primary" routerLink="/upload">
    Upload Video
  </a>
</div>


  <div class="row">
    <div class="col-md-4 mb-4" *ngFor="let video of videos">
      <div class="card shadow-sm h-100">
        <video controls class="card-img-top" [src]="video.blobUrl" style="height: 200px; object-fit: cover;"></video>
        <div class="card-body">
          <h5 class="card-title">{{ video.title }}</h5>
          <p class="card-text">{{ video.description }}</p>
          <small class="text-muted">Uploaded on {{ video.uploadDate | date: 'mediumDate' }}</small>
        </div>
      </div>
    </div>
  </div>

  <p *ngIf="!videos.length" class="text-center text-muted">No videos uploaded yet.</p>
</div>
```

### Visit: http://localhost:4200/

![alt text](image-78.png)

![alt text](image-79.png)

![alt text](image-80.png)

![alt text](image-81.png)
