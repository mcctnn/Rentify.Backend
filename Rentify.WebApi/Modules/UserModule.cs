using MediatR;
using Rentify.Application.Items;
using Rentify.Application.Users;
using Rentify.Domain.Items;
using Rentify.Domain.Users;
using TS.Result;

namespace Rentify.WebApi.Modules;

public static class UserModule
{
    public static void RegisterUserRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/users").WithTags("Users").RequireAuthorization();

        group.MapPost(string.Empty,
            async (ISender sender, CreateUserCommand request, CancellationToken token) =>
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
                var response = await sender.Send(new GetUserQuery(id), token);

                return response.IsSuccessful
                ? Results.Ok(response)
                : Results.InternalServerError(response);
            })
            .Produces<Result<User>>();
    }
}
