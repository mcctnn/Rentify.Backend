using MediatR;
using Microsoft.AspNetCore.Identity;
using Rentify.Domain.Users;
using TS.Result;

namespace Rentify.Application.Users;
public sealed record GetAllUserQuery(
    ):IRequest<Result<List<User>>>;


internal sealed class GetAllUserQueryHandler(
    UserManager<User> userManager) : IRequestHandler<GetAllUserQuery, Result<List<User>>>
{
    public async Task<Result<List<User>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Roller
            var allowedRoles = new List<string> { "Seller", "NormalUser" };

            // Seller ve NormalUser kullanıcıları al
            var users = userManager.Users.ToList();

            var filteredUsers = new List<User>();

            foreach (var user in users)
            {
                var userRoles = await userManager.GetRolesAsync(user);

                if (userRoles.Any(role => allowedRoles.Contains(role)))
                {
                    filteredUsers.Add(user);
                }
            }

            return Result<List<User>>.Succeed(filteredUsers);
        }
        catch (Exception ex)
        {
            return Result<List<User>>.Failure(ex.Message);
        }
    }
}
