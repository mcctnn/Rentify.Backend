using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rentify.Domain.Items;
using Rentify.Domain.Users;
using TS.Result;

namespace Rentify.Application.Users;
public sealed record GetItemsByUserIdQuery(
    Guid Id):IRequest<Result<UserDto>>;

public sealed class UserDto
{
    public Guid Id { get; set; }
    public List<ItemDto>? Items { get; set; }
}

public sealed record ItemDto(
    string Name,
    string Description,
    decimal PricePerDay);

internal sealed class GetItemsByUserIdQueryHandler(
    UserManager<User> userManager,
    IItemRepository itemRepository) : IRequestHandler<GetItemsByUserIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetItemsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());

        if (user is null)
        {
            return Result<UserDto>.Failure("User not found");
        }

        List<Item> oldItems = await itemRepository.Where(p => p.UserId == request.Id).ToListAsync();

        var items = oldItems.Select(s => new ItemDto(
            s.Name,
            s.Description,
            s.PricePerDay
        )).ToList();

        var userDto = new UserDto
        {
            Id = user.Id,
            Items = items
        };

        return Result<UserDto>.Succeed(userDto);
    }
}

