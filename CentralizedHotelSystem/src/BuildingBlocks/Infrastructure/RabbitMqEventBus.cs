namespace BuildingBlocks.Infrastructure;

public class RabbitMqEventBus
{
    public void Publish(string eventType, string payload)
    {
        Console.WriteLine($"🐇 Published to RabbitMQ: {eventType}");
        // Add RabbitMQ producer logic here
    }
}
