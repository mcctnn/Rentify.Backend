using Microsoft.AspNetCore.Identity;

namespace Rentify.Domain.Users;
public sealed class Role : IdentityRole<Guid>
{
    public Role() : base() { }

    public Role(string roleName) : base(roleName) { }

    public UserRole UserRole { get; set; }
}

