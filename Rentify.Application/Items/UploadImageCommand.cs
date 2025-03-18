using MediatR;
using Microsoft.AspNetCore.Http;
using Rentify.Application.Services;
using TS.Result;

namespace Rentify.Application.Items;

public sealed record UploadImageCommand(
    IFormFile File) : IRequest<Result<string>>;

internal sealed class UploadImageCommandHandler(
    IFileService fileService) : IRequestHandler<UploadImageCommand, Result<string>>
{
    public async Task<Result<String>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.Length == 0)
        {
            return Result<string>.Failure("File is empty");
        }

        var fileUrl = await fileService.UploadFileAsync(request.File);
        return Result<string>.Succeed(fileUrl);
    }
}
