using ABS.FileGeneration.Exceptions;
using ABS.FileGeneration.Interfaces;
using OfficeOpenXml;

namespace ABS.FileGeneration.Services
{
    public class ExcelFileGenerator : IFileGenerator
    {
        private readonly IFileService _fileService;

        public ExcelFileGenerator(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<FileStream> GenerateFileAsync<T>(string fileName, IEnumerable<T> data, 
                                        CancellationToken cancellationToken = default)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            const string defaultSheetName = "sheet1";

            if (!data.Any())
            {
                throw new ArgumentException("The data source is invalid", nameof(data));
            }

            try
            {
                using var package = new ExcelPackage();
                var workSheet = package.Workbook.Worksheets.Add(defaultSheetName);
                var properties = typeof(T).GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    workSheet.Cells[1, i + 1].Value = properties[i].Name;
                }

                int row = 2;
                foreach (var item in data)
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        workSheet.Cells[row, i + 1].Value = properties[i].GetValue(item);
                    }
                    row++;
                }

                // Save file in a temp folder, not memory, avoid memmory overloading, 
                // File in temp folder will be destroyed automatically.
                workSheet.Cells.AutoFitColumns();
                string filePath = Path.Combine(Path.GetTempPath(), fileName); 
                await _fileService.SaveAsync(package, filePath);

                return _fileService.OpenRead(filePath);
            }
            catch (IOException ex)
            {
                throw new FileGenerationException("Error during file I/O operations", ex);
            }
            catch (OutOfMemoryException ex)
            {
                throw new FileGenerationException("Insufficient memory for file generation", ex);
            }
            catch (Exception ex)
            {
                throw new FileGenerationException("Unexpected error during file generation", ex);
            }
        }
    }
}

