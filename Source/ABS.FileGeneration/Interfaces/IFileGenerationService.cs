namespace ABS.FileGeneration.Interfaces
{
    public interface IFileGenerationService
    {
        public Task<FileStream> GenerateFileAsync(string fileName, CancellationToken cancellationToken = default);

    }
}
