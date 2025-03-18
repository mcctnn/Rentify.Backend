using Microsoft.AspNetCore.Http;

namespace Rentify.Application.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file);
}
