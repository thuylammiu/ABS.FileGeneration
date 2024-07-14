using ABS.FileGeneration.Interfaces;
using ABS.FileGeneration.Services;
using ABS.FileGenerationAPI.ExampleData;
using ABS.FileGenerationAPI.Interfaces;

namespace ABS.FileGenerationAPI.Services
{
    internal class EmployeeGenerationService : FileGenerationService<Employee>, IEmployeeGenerationService
    {
        public EmployeeGenerationService(IDataSourceProvider<Employee> dataSourceProvider, IFileGenerator fileGenerator) : base(dataSourceProvider, fileGenerator)
        {
            //This is for future enhancement, This can write code if Employee have other specific features like MoveFile, BackupFile....
        }


    }
}
