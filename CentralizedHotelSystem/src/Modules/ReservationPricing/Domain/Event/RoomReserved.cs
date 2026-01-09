using BuildingBlocks.Domain;

namespace Modules.ReservationPricing.Domain.Events;

public class RoomReserved : DomainEvent
{
    public Guid ReservationId { get; }
    public string CustomerName { get; }

    public RoomReserved(Guid reservationId, string customerName)
    {
        ReservationId = reservationId;
        CustomerName = customerName;
    }
}
