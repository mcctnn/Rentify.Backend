using MediatR;
using Microsoft.AspNetCore.Identity;
using Rentify.Domain.Users;
using TS.Result;

namespace Rentify.Application.Users;
public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    string Email,
    string Location):IRequest<Result<string>>;

internal sealed class CreateUserCommandHandler(
    UserManager<User> userManager) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Kullanıcı oluştur
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            Location = request.Location
        };

        // Kullanıcıyı oluştur ve şifresini ata
        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            return Result<string>.Failure($"User creation failed: {errors}");
        }

        // Kullanıcının rolünü belirle (Veritabanındaki büyük harf formatına uygun olacak şekilde)
        string role = request.UserName.StartsWith("seller_", StringComparison.OrdinalIgnoreCase) ? "SELLER" : "NORMALUSER";

        // Kullanıcıya rol ata
        var roleResult = await userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            return Result<string>.Failure("Failed to assign role.");
        }

        return Result<string>.Succeed(user.Id.ToString());
    }
}

