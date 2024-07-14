using ABS.FileGeneration.Interfaces;
using OfficeOpenXml;

namespace ABS.FileGeneration.Services
{
    internal class FileService : IFileService
    {
        public async Task SaveAsync(ExcelPackage package, string filePath)
        {
            await package.SaveAsAsync(new FileInfo(filePath));
        }
        public FileStream OpenRead(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}
