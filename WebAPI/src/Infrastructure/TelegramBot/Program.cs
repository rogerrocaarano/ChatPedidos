using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using TelegramBot.Configurations;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddApplication();
builder.Services.AddFastEndpoints().SwaggerDocument();

builder.Services.AddTelegramBot(builder.Configuration);

var app = builder.Build();
app.UseFastEndpoints(config =>
    {
        config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    })
    .UseSwaggerGen();

app.Run();
