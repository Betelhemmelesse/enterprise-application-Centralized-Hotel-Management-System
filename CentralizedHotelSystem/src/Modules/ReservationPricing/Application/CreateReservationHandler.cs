using MediatR;
using Modules.ReservationPricing.Domain;
using Modules.ReservationPricing.Infrastructure;

namespace Modules.ReservationPricing.Application;

public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly ReservationDbContext _db;

    public CreateReservationHandler(ReservationDbContext db) => _db = db;

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = new RoomReservation(request.CustomerName);
        _db.Reservations.Add(reservation);

        foreach (var e in reservation.Events)
        {
            _db.OutboxMessages.Add(new BuildingBlocks.Infrastructure.OutboxMessage
            {
                Type = e.GetType().Name,
                Payload = System.Text.Json.JsonSerializer.Serialize(e)
            });
        }

        await _db.SaveChangesAsync(cancellationToken);
        return reservation.Id;
    }
}
