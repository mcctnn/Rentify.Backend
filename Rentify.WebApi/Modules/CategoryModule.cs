using MediatR;
using Rentify.Application.Categories;
using Rentify.Domain.Categories;
using TS.Result;

namespace Rentify.WebApi.Modules;

public static class CategoryModule
{
    public static void RegisterCategoryRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/categories").WithTags("Categories");

        group.MapPost(string.Empty,
            async (ISender sender, CreateCategoryCommand request, CancellationToken token) =>
            {
                var response = await sender.Send(request, token);

                return response.IsSuccessful
                ? Results.Ok(response)
                : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        group.MapPut("{id}",
            async (ISender sender, UpdateCategoryCommand request, CancellationToken token) =>
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
                var response = await sender.Send(new GetCategoryQuery(id), token);

                return response.IsSuccessful
                ? Results.Ok(response)
                : Results.InternalServerError(response);
            })
            .Produces<Result<Category>>();

        group.MapDelete("{id}",
            async (ISender sender, Guid id, CancellationToken token) =>
            {
                var response = await sender.Send(new DeleteCategoryCommand(id), token);

                return response.IsSuccessful
                ? Results.Ok(response)
                : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }
}
