using BankAPIV7.Services;
using BankAPIV7.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPIV7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService fileService;
        private readonly ILogger<FilesController> _logger;

        public FilesController(IFileService fileService, ILogger<FilesController> logger)
        {
            this.fileService = fileService;
            _logger = logger;
        }


        [HttpPost("upload-multipartreader")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [MultipartFormData]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            Random random = new Random();
            var randomValue = random.Next(0, 1000);
            var fileUploadSummary = await fileService.UploadFileAsync(HttpContext.Request.Body, Request.ContentType);
            _logger.LogInformation($"Random Value is {randomValue}");
            return CreatedAtAction(nameof(Upload), fileUploadSummary);
        }

    }
}
