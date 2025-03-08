using GenericRepository;
using MediatR;
using Rentify.Domain.Items;
using TS.Result;

namespace Rentify.Application.Items;

public sealed record DeleteItemCommand(
    Guid Id):IRequest<Result<string>>;

internal sealed class DeleteItemCommandHandler(
   IItemRepository itemRepository,
   IUnitOfWork unitOfWork) : IRequestHandler<DeleteItemCommand, Result<string>>
{
    public async Task<Result<String>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var item = await itemRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (item is null)
            return Result<string>.Failure("Failed");

        itemRepository.Delete(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Failure("Item has been deleted success");
    }
}
