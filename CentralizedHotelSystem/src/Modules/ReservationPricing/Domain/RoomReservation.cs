using BuildingBlocks.Domain;
using Modules.ReservationPricing.Domain.Events;

namespace Modules.ReservationPricing.Domain;

public class RoomReservation : AggregateRoot
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CustomerName { get; private set; } = default!;
    public DateTime ReservedAt { get; private set; }

    public RoomReservation(string customerName)
    {
        CustomerName = customerName;
        ReservedAt = DateTime.UtcNow;
        AddEvent(new RoomReserved(Id, customerName));
    }
}
