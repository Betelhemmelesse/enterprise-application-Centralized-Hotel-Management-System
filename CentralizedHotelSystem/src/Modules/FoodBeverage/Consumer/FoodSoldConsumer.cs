using Modules.FoodBeverage.Domain.Events;

namespace Modules.FoodBeverage.Consumer;

public class FoodSoldConsumer
{
    public void Handle(FoodSold @event)
    {
        Console.WriteLine($"🍽 Food sold: {@event.FoodName} ({@event.FoodId})");
    }
}
