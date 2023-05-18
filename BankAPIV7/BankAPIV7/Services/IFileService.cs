using BankAPIV7.Models;

namespace BankAPIV7.Services
{
    public interface IFileService
    {
        Task<FileUploadSummary> UploadFileAsync(Stream fileStream, string contentType);
    }
}
