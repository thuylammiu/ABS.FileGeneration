using ABS.FileGeneration.Interfaces;
using ABS.FileGeneration.Services;
using ABS.FileGenerationAPI.Data;
using ABS.FileGenerationAPI.ExampleData;
using ABS.FileGenerationAPI.Interfaces;
using ABS.FileGenerationAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ABS.FileGenerationAPI.Extensions
{
    public static class ServiceRegisterExtension
    {
        public static IServiceCollection RegisterGenrationFileTypeService(this IServiceCollection services, IConfiguration config)
        {          
           
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IFileGenerator, ExcelFileGenerator>();
            services.AddScoped<IDataSourceProvider<Employee>, EmployeeFileDataProvider>();
            services.AddScoped<IEmployeeGenerationService, EmployeeGenerationService>();
            
            return services;
        }
    }
}
