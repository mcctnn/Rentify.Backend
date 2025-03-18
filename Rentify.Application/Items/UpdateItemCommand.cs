using GenericRepository;
using Mapster;
using MediatR;
using Rentify.Domain.Items;
using TS.Result;

namespace Rentify.Application.Items;

public sealed record UpdateItemCommand(
    Guid Id,
    Guid UserId,
    string Name,
    string ItemImageUrl,
    string Description,
    decimal PricePerDay,
    Guid CategoryId,
    string Location) :IRequest<Result<string>>;

internal sealed class UpdateItemCommandHandler(
    IItemRepository itemRepository,
    IUnitOfWork unitOfWork):IRequestHandler<UpdateItemCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        Item? item=await itemRepository.FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);

        if (item is null)
            return Result<string>.Failure("Item not found");

        item.Adapt(request);

        itemRepository.Update(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Item updated successful";
    }
}
