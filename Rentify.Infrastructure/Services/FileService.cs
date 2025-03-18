using Microsoft.AspNetCore.Http;
using Rentify.Application.Services;

namespace Rentify.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

    public FileService()
    {
        if (!Directory.Exists(_uploadPath))
        {
            Directory.CreateDirectory(_uploadPath);
        }
    }
    public async Task<String> UploadFileAsync(IFormFile file)
    {
        var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_uploadPath, fileName);

        using (var stream=new FileStream(filePath,FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/uploads/{fileName}";
    }
}
