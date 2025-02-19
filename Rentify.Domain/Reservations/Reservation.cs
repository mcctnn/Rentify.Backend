using Rentify.Domain.Abstractions;
using Rentify.Domain.Items;
using Rentify.Domain.Users;

namespace Rentify.Domain.Reservations;
public sealed class Reservation:Entity
{
    public Guid UserId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ReservationStatusEnum Status { get; set; } = ReservationStatusEnum.Pending; // "Pending", "Approved", "Cancelled"
    public User? User { get; set; }
    public Item? Item { get; set; }
}
