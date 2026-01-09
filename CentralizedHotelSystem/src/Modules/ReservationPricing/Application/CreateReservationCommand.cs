using MediatR;

namespace Modules.ReservationPricing.Application;

public record CreateReservationCommand(string CustomerName) : IRequest<Guid>;
