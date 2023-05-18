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

        public FilesController(IFileService fileService)
        {
            this.fileService = fileService;
        }


        [HttpPost("upload-multipartreader")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [MultipartFormData]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            var fileUploadSummary = await fileService.UploadFileAsync(HttpContext.Request.Body, Request.ContentType);

            return CreatedAtAction(nameof(Upload), fileUploadSummary);
        }

    }
}
