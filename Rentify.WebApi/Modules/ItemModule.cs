using MediatR;
using Rentify.Application.Items;
using Rentify.Domain.Items;
using TS.Result;

namespace Rentify.WebApi.Modules;

public static class ItemModule
{
    public static void RegisterItemRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/items").WithTags("Items").RequireAuthorization();

        group.MapPost(string.Empty,
            async (ISender sender, CreateItemCommand request, CancellationToken token) =>
            {
                var response = await sender.Send(request, token);

                return response.IsSuccessful
                ? Results.Ok(response)
                : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        group.MapGet(string.Empty,
            async (ISender sender, Guid id, CancellationToken token) =>
            {
                var response = await sender.Send(new GetItemQuery(id), token);

                return response.IsSuccessful
                ? Results.Ok(response)
                : Results.InternalServerError(response);
            })
            .Produces<Result<Item>>();

        group.MapDelete("{id}",
            async (ISender sender, Guid id, CancellationToken token) =>
            {
                var response = await sender.Send(new DeleteItemCommand(id), token);

                return response.IsSuccessful
                ? Results.Ok(response)
                : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }
}
