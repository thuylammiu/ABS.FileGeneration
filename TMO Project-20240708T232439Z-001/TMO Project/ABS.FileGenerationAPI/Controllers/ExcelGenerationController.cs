using ABS.FileGeneration.Exceptions;
using ABS.FileGenerationAPI.Interfaces;
using ABS.FileGenerationAPI.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABS.FileGenerationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ExcelGenerationController : ControllerBase
    {
        private readonly IEmployeeGenerationService _fileGenerationService;
        private readonly ILogger<ExcelGenerationController> _logger;
        public ExcelGenerationController(IEmployeeGenerationService fileGenerationService,
                                        ILogger<ExcelGenerationController> logger)
        {
            this._fileGenerationService = fileGenerationService;
            _logger = logger;

        }

        [HttpPost("server/download")]
        [Authorize]
        public async Task<IActionResult> GenerateExcelServer([FromBody] FileGenerationDataRequest bodyRequest)
        {

            return await GenerateExcel(bodyRequest);
        }

        [HttpPost("client/download")]        
        public async Task<IActionResult> GenerateExcelClient([FromBody] FileGenerationDataRequest bodyRequest)
        {

            return await GenerateExcel(bodyRequest);
        }


        private async Task<IActionResult> GenerateExcel( FileGenerationDataRequest bodyRequest)
        {

            if (string.IsNullOrWhiteSpace(bodyRequest.FileName))
            {
                return BadRequest("File type are required.");
            }

            try
            {
                var fileStream = await _fileGenerationService.GenerateFileAsync(bodyRequest.FileName,
                                                                                bodyRequest.Cancellation);

                if (fileStream == null || fileStream.Length == 0)
                {
                    return StatusCode(204, "File generation failed.");
                }

                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                        bodyRequest.FileName);

            }
            catch (FileGenerationException ex)
            {
                _logger.LogError(ex, "Error generating Excel file");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during file generation");
                throw;
            }
        }

    }
}
