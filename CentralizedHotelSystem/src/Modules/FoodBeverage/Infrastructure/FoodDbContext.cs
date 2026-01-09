using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Infrastructure;
using Modules.FoodBeverage.Domain;

namespace Modules.FoodBeverage.Infrastructure;

public class FoodDbContext : DbContext, IOutboxDbContext
{
    public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) {}

    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
}
