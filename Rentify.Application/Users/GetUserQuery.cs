using MediatR;
using Microsoft.AspNetCore.Identity;
using Rentify.Domain.Users;
using TS.Result;

namespace Rentify.Application.Users;
public sealed record GetUserQuery(
    Guid Id):IRequest<Result<User>>;


internal sealed class GetUserQueryHandler(
    UserManager<User> userManager) : IRequestHandler<GetUserQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Kullanıcıyı UserId'ye göre bulma
            var user = await userManager.FindByIdAsync(request.Id.ToString());

            if (user is null)
            {
                return Result<User>.Failure("Kullanıcı bulunamadı.");
            }

            return Result<User>.Succeed(user);
        }
        catch (Exception ex)
        {
            return Result<User>.Failure($"Bir hata oluştu: {ex.Message}");
        }
    }
}
