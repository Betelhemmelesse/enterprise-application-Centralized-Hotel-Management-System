using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Infrastructure;
using Modules.CustomerManagement.Domain;

namespace Modules.CustomerManagement.Infrastructure;

public class CustomerDbContext : DbContext, IOutboxDbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) {}

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
}
