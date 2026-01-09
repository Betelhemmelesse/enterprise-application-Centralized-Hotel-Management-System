using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure;

public class OutboxPublisherJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OutboxPublisherJob(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();
            var messages = await db.OutboxMessages
                .Where(x => x.ProcessedOn == null)
                .OrderBy(x => x.OccurredOn)
                .ToListAsync(stoppingToken);

            foreach (var message in messages)
            {
                Console.WriteLine($"📤 Publishing event: {message.Type}");
                // RabbitMQ integration can go here
                message.ProcessedOn = DateTime.UtcNow;
            }

            await db.SaveChangesAsync(stoppingToken);
            await Task.Delay(5000, stoppingToken);
        }
    }
}
