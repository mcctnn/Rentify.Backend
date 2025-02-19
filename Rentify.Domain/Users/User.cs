using Microsoft.AspNetCore.Identity;
using Rentify.Domain.Items;
using Rentify.Domain.Reservations;

namespace Rentify.Domain.Users;
public sealed class User : IdentityUser<Guid>
{
    public User()
    {
        Id = Guid.CreateVersion7();
    }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public decimal Balance { get; set; } = 0;
    public string Location { get; set; } = default!;
    public ICollection<Item>? Items { get; set; }
    public ICollection<Reservation>? Reservations { get; set; }
}
