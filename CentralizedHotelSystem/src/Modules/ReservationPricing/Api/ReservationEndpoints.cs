using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MediatR;
using Modules.ReservationPricing.Application;
using Modules.ReservationPricing.Infrastructure;

namespace Modules.ReservationPricing.Api;

public static class ReservationEndpoints
{
    public static void MapReservationEndpoints(this WebApplication app)
    {
        app.MapPost("/reservations", async (IMediator mediator, string customerName) =>
        {
            var cmd = new CreateReservationCommand(customerName);
            var id = await mediator.Send(cmd);
            return Results.Ok(new { ReservationId = id });
        }).RequireAuthorization();
    }
}
