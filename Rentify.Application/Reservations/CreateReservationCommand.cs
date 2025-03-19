using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Rentify.Domain.Items;
using Rentify.Domain.Reservations;
using Rentify.Domain.Users;
using TS.Result;

namespace Rentify.Application.Reservations;

public sealed record CreateReservationCommand(
    Guid UserId,
    Guid ItemId,
    DateTime StartDate,
    DateTime EndDate):IRequest<Result<string>>;


internal sealed class CreateReservationCommandHandler(
    IItemRepository itemRepository,
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateReservationCommand, Result<string>>
{
    public async Task<Result<String>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var item = await itemRepository.FirstOrDefaultAsync(p=>p.Id==request.ItemId);

        if (item == null)
        {
            return Result<string>.Failure("Item not found");
        }


    }
}
