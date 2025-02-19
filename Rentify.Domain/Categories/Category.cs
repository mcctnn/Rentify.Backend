using Rentify.Domain.Abstractions;

namespace Rentify.Domain.Categories;
public sealed class Category:Entity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
