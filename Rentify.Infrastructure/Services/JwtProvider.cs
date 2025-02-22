using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rentify.Application.Services;
using Rentify.Domain.Users;
using Rentify.Infrastructure.Context;
using Rentify.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rentify.Infrastructure.Services;
internal sealed class JwtProvider(IOptions<JwtOptions> options, UserManager<User> userManager, ApplicationDbContext context) : IJwtProvider
{
    public async Task<string> CreateTokenAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!)
        };


        var userRoles = await context.UserRoles
        .Where(ur => ur.UserId == user.Id)
        .Join(context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
        .ToListAsync();

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var expires = DateTime.Now.AddDays(1);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(options.Value.SecretKey));

        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken securityToken = new(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(securityToken);

        return token;
    }
}
