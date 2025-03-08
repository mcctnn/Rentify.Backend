using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rentify.Domain.Categories;
using Rentify.Domain.Items;
using Rentify.Domain.Users;
using TS.Result;

namespace Rentify.Application.Items;
public sealed record GetAllItemQuery() : IRequest<List<Item>>;

public sealed class ItemDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string ItemImageUrl { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal PricePerDay { get; set; }
    public Guid CategoryId { get; set; }
    public string Location { get; set; } = default!;
    public bool IsAvailable { get; set; } = false;
    public string CategoryName { get; set; } = default!;
    public string FullName { get; set; } = default!;
}

internal sealed class GetAllItemQueryHandler(
    IItemRepository itemRepository) : IRequestHandler<GetAllItemQuery, List<Item>>
{
    public async Task<List<Item>> Handle(GetAllItemQuery request, CancellationToken cancellationToken)
    {
        var items = await itemRepository.GetAll()
            .Include(i => i.Owner)
            .Include(i => i.Category)  
            .ToListAsync();

        return items;
    }

}        



