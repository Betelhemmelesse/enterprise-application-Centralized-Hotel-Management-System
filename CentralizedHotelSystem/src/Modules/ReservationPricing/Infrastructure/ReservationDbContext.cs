using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Infrastructure;
using Modules.ReservationPricing.Domain;

namespace Modules.ReservationPricing.Infrastructure;

public class ReservationDbContext : DbContext, IOutboxDbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options) {}

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<RoomReservation> Reservations => Set<RoomReservation>();
}
