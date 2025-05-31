using Microsoft.EntityFrameworkCore;
using MoodJournal.Contexts;
using MoodJournal.Interfaces;
using MoodJournal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<MoodJournalContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register Services
builder.Services.AddScoped<ILLMService, LLMService>();
builder.Services.AddScoped<LLMService>();

builder.Services.AddHttpClient<LLMService>(client =>
{
    var url = builder.Configuration["LLMSettings:BaseUrl"];
    client.BaseAddress = new Uri(url!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();