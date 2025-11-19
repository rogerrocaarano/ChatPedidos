using Application;
using FastEndpoints;
using FastEndpoints.Swagger;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddFastEndpoints().SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints().UseSwaggerGen();

app.Run();
