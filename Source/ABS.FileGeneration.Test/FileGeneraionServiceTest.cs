using ABS.FileGeneration.Interfaces;
using ABS.FileGeneration.Services;
using Moq;

namespace ABS.FileGeneration.Test
{
    public class FileGeneraionServiceTest
    {
        private readonly Mock<IDataSourceProvider<object>> _dataSourceProviderMock;
        private readonly Mock<IFileGenerator> _fileGeneratorMock;

        public FileGeneraionServiceTest()
        {
            _dataSourceProviderMock = new Mock<IDataSourceProvider<object>>();
            _fileGeneratorMock = new Mock<IFileGenerator>();
        }

        [Fact]
        public async Task GenerateFileAsync_ShouldThrowInvalidOperationException_WhenFileGeneratorIsNull()
        {
            // Arrange
            var service = new TestFileGenerationService(_dataSourceProviderMock.Object, null);
            string fileType = "xlsx";
            string fileName = "test.xlsx";

            // Act & Assert
            CancellationToken cancellationToken = CancellationToken.None;
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.GenerateFileAsync(fileName, cancellationToken));
            Assert.Equal("File Generator must be provided before generating a file.", exception.Message);
        }

        [Fact]
        public async Task GenerateFileAsync_ShouldCallGenerateFileAsyncOnFileGenerator_WithCorrectParameters()
        {
            // Arrange
            var data = new List<object> { new { Name = "Test" } };
            _dataSourceProviderMock.Setup(d => d.GetData()).Returns(data);

            var service = new TestFileGenerationService(_dataSourceProviderMock.Object, _fileGeneratorMock.Object);
            string fileType = "xlsx";
            string fileName = "test.xlsx";

            var fileStream = new MemoryStream();
            CancellationToken cancellationToken = CancellationToken.None;
            _fileGeneratorMock.Setup(g => g.GenerateFileAsync(fileName, data, cancellationToken)).ReturnsAsync(new FileStream(fileName, FileMode.Create));

            // Act
            var result = await service.GenerateFileAsync(fileName, cancellationToken);

            // Assert
            Assert.NotNull(result);
            _fileGeneratorMock.Verify(g => g.GenerateFileAsync(fileName, data, cancellationToken), Times.Once);
        }

        // Helper class to test the abstract FileGenerationService class
        private class TestFileGenerationService : FileGenerationService<object>
        {
            public TestFileGenerationService(IDataSourceProvider<object> dataSourceProvider, IFileGenerator fileGenerator)
                : base(dataSourceProvider, fileGenerator)
            {
            }
        }
    }
}
