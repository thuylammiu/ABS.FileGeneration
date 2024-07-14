using OfficeOpenXml;

namespace ABS.FileGeneration.Interfaces
{
    public interface IFileService
    {
        Task SaveAsync(ExcelPackage package, string filePath);
        FileStream OpenRead(string filePath);
    }
}
