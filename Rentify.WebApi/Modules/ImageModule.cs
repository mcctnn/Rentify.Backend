using MediatR;
using Rentify.Application.Items;
using TS.Result;

namespace Rentify.WebApi.Modules;

public static class ImageModule
{
    public static void MapImageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/images");

        group.MapPost(string.Empty, async (ISender sender, IFormFile file, CancellationToken token) =>
        {
            var command = new UploadImageCommand(file);
            var response = await sender.Send(command, token);

            return response.IsSuccessful
                ? Results.Ok(response)
                : Results.BadRequest(response);
        })
        .Produces<Result<string>>()
        .DisableAntiforgery(); // Dosya yükleme için anti-forgery devre dışı bırakılır
    }
}
