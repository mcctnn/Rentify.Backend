using MediatR;
using Rentify.Domain.Items;
using TS.Result;

namespace Rentify.Application.Items;
public sealed record GetItemQuery(
    Guid Id):IRequest<Result<Item>>;

internal sealed class GetItemQueryHandler(
    IItemRepository itemRepository) : IRequestHandler<GetItemQuery, Result<Item>>
{
    public async Task<Result<Item>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var item = await itemRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (item is null)
            return Result<Item>.Failure("Eşya bulunamadı");

        return item;
    }
}
