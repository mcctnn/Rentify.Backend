using GenericRepository;
using Rentify.Domain.Reservations;
using Rentify.Infrastructure.Context;

namespace Rentify.Infrastructure.Repositories;
internal sealed class ReservationRepository : Repository<Reservation, ApplicationDbContext>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
