using ABS.FileGeneration.Interfaces;

namespace ABS.FileGeneration.Services
{
    public  class FileGenerationService<T> : IFileGenerationService where T : class
    {

        private readonly IDataSourceProvider<T> _dataSourceProvider;
        private readonly IFileGenerator _fileGenerator;
        public FileGenerationService(IDataSourceProvider<T> dataSourceProvider, IFileGenerator fileGenerator)
        {
            _dataSourceProvider = dataSourceProvider;
            _fileGenerator = fileGenerator;
        }

        public async virtual Task<FileStream> GenerateFileAsync(string fileName, CancellationToken cancellationToken= default)
        {
            if (_fileGenerator == null)
            {
                throw new InvalidOperationException("File Generator must be provided before generating a file.");
            }
            var data = _dataSourceProvider.GetData();
            return await _fileGenerator.GenerateFileAsync(fileName, data);
        }
    }
}