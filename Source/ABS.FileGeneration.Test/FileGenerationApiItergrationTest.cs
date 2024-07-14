using ABS.FileGenerationAPI.Models.Requests;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ABS.FileGeneration.Test
{
    public class FileGenerationApiItergrationTest : IClassFixture<WebApiTestFixture>
    {
        private readonly HttpClient client;
        CancellationToken cancellationToken = new CancellationToken();
        public FileGenerationApiItergrationTest(WebApiTestFixture factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task GenerateExcel_ReturnsBadRequest_WhenFileTypeMissing()
        {
            // Arrange

            var bodyRequest = new FileGenerationDataRequest { Cancellation= cancellationToken, FileName = "test.xlsx" };

            // Act
            var response = await client.PostAsync("/api/FileGeneration/generateExcel", GetStringContent(bodyRequest));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task GenerateExcel_ReturnsFile_WhenGenerationSuccessful()
        {
            // Arrange

            var bodyRequest = new FileGenerationDataRequest { Cancellation = cancellationToken, FileName = "test.xlsx" };

            // Act
            var response = await client.PostAsync("/api/FileGeneration/generateExcel", GetStringContent(bodyRequest));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", response.Content.Headers.ContentType.MediaType);

        }

        [Fact]
        public async Task GenerateExcel_ReturnsServerError_WhenFileGenerationFails()
        {
            // Arrange
            
           
            var bodyRequest = new FileGenerationDataRequest { Cancellation = cancellationToken, FileName = "test.xlsx" };

            // Act
            var response = await client.PostAsync("/api/FileGeneration/generateExcel", GetStringContent(bodyRequest));

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        private StringContent GetStringContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
