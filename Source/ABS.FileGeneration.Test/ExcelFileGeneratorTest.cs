using ABS.FileGeneration.Exceptions;
using ABS.FileGeneration.Interfaces;
using ABS.FileGeneration.Services;
using Moq;
using OfficeOpenXml;

namespace ABS.FileGeneration.Test
{
    public class ExcelFileGeneratorTest    {
        
        [Fact]
        public async Task GenerateFileAsync_ShouldThrowArgumentNullException_WhenDataIsNull()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var generator = new ExcelFileGenerator(fileServiceMock.Object);
            string fileName = "test.xlsx";
            IEnumerable<object> data = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => generator.GenerateFileAsync(fileName, data));
            Assert.Equal("source", exception.ParamName);
        }

        [Fact]
        public async Task GenerateFileAsync_ShouldThrowArgumentNullException_WhenDataIsEmpty()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var generator = new ExcelFileGenerator(fileServiceMock.Object);
            string fileName = "test.xlsx";
            var data = Enumerable.Empty<object>();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => generator.GenerateFileAsync(fileName, data));
            Assert.Equal("data", exception.ParamName);
        }

        [Fact]
        public async Task GenerateFileAsync_ShouldThrowFileGenerationException_WhenIOExceptionOccurs()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var generator = new ExcelFileGenerator(fileServiceMock.Object);
            string fileName = "test.xlsx";
            var data = new List<object> { new { Name = "Test" } };

            fileServiceMock
                .Setup(f => f.SaveAsync(It.IsAny<ExcelPackage>(), It.IsAny<string>()))
                .ThrowsAsync(new IOException());

            await Assert.ThrowsAsync<FileGenerationException>(async () =>
            {
                await generator.GenerateFileAsync(fileName, data);
            });

        }

        [Fact]
        public async Task GenerateFileAsync_ShouldThrowFileGenerationException_WhenOutOfMemoryExceptionOccurs()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var generator = new ExcelFileGenerator(fileServiceMock.Object);
            string fileName = "test.xlsx";
            var data = new List<object> { new { Name = "Test" } };

            fileServiceMock
                .Setup(f => f.SaveAsync(It.IsAny<ExcelPackage>(), It.IsAny<string>()))
                .ThrowsAsync(new OutOfMemoryException());


            var exception = await Assert.ThrowsAsync<FileGenerationException>(async () =>
            {
                await generator.GenerateFileAsync(fileName, data);
            });
            Assert.Contains("Insufficient memory for file generation", exception.Message);
        }

        [Fact]
        public async Task GenerateFileAsync_ShouldThrowFileGenerationException_WhenUnexpectedExceptionOccurs()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var generator = new ExcelFileGenerator(fileServiceMock.Object);
            string fileName = "test.xlsx";
            var data = new List<object> { new { Name = "Test" } };

            fileServiceMock
                .Setup(f => f.SaveAsync(It.IsAny<ExcelPackage>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var exception = await Assert.ThrowsAsync<FileGenerationException>(async () =>
            {
                await generator.GenerateFileAsync(fileName, data);
            });
            Assert.Contains("Unexpected error during file generation", exception.Message);
        }

        [Fact]
        public async Task GenerateFileAsync_ShouldReturnFileStream_WhenDataIsValid()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var generator = new ExcelFileGenerator(fileServiceMock.Object);
            string fileName = "test.xlsx";
            var data = new List<object> { new { Name = "Test" } };
            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            fileServiceMock
                .Setup(f => f.SaveAsync(It.IsAny<ExcelPackage>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            fileServiceMock
                .Setup(f => f.OpenRead(filePath))
                .Returns(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));

            // Act
            var result = await generator.GenerateFileAsync(fileName, data);

            // Assert
            Assert.NotNull(result);
            fileServiceMock.Verify(f => f.SaveAsync(It.IsAny<ExcelPackage>(), filePath), Times.Once);
            fileServiceMock.Verify(f => f.OpenRead(filePath), Times.Once);
        }

    }
}
