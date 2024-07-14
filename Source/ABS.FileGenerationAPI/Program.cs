using ABS.FileGeneration;
using ABS.FileGenerationAPI.Data;
using ABS.FileGenerationAPI.Extensions;
using ABS.FileGenerationAPI.Middleware;
using ABS.FileGenerationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(config =>
{
});

builder.Services.AddControllers();
builder.Services.UseFileGenerationService();
builder.Services.RegisterGenrationFileTypeService(builder.Configuration);
builder.Services.AddCustomJwtAthuExtension(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpLogging();
app.UseSwaggerUI();
app.UseSwagger();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors(); app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("https://localhost:44331"));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();   
    await context.Database.MigrateAsync();   
    
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();

public partial class Program { }