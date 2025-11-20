using System.Text.Json;
using Application;
using FastEndpoints;
using FastEndpoints.Swagger;
using Persistence;
using TelegramBot.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddTelegramBot(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddFastEndpoints().SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints(config =>
    {
        config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    })
    .UseSwaggerGen();

app.Run();
