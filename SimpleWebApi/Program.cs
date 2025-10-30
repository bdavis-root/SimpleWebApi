using Azure.Core.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SimpleWebApi.Data;
using SimpleWebApi.Exceptions;

using AzureEventSourceListener listener = AzureEventSourceListener.CreateConsoleLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<SimpleWebApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.")));

builder.Services
    .AddOpenApi()
    .AddControllers();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder
    .Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.MapControllers();
app.Run();
