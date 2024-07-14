namespace ABS.FileGeneration.Interfaces
{
    /*
     This approach enhances the flexibility of the data source, 
     allowing the client to decide which data source to use. 
     Therefore, the injection of this interface will occur on the client side (in this case, the web API).
     */
    public interface IFileGenerator
    {
        Task<FileStream> GenerateFileAsync<T>(string fileName, IEnumerable<T> data, CancellationToken cancellationToken = default);
    }
}
