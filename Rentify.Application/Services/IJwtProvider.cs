using Rentify.Domain.Users;

namespace Rentify.Application.Services;
public interface IJwtProvider
{
    public Task<string> CreateTokenAsync(User user, string password, CancellationToken cancellationToken = default);
}
