namespace ABS.FileGeneration.Exceptions
{
    public class FileGenerationException : Exception
    {
        public FileGenerationException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
    }
}
