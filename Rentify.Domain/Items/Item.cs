using Rentify.Domain.Abstractions;
using Rentify.Domain.Categories;
using Rentify.Domain.Users;

namespace Rentify.Domain.Items;
public sealed class Item:Entity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string ItemImageUrl { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal PricePerDay { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public string Location { get; set; } = default!;
    public bool IsAvailable { get; set; } = false;
    public User? Owner { get; set; }
}
