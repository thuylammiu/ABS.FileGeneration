
using ABS.FileGeneration.Interfaces;
using ABS.FileGeneration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ABS.FileGeneration;

public static class PackageExports
{
    public static IServiceCollection UseFileGenerationService(this IServiceCollection services)
    {

        services.AddScoped<IFileService, FileService>();
        return services;
    }
}