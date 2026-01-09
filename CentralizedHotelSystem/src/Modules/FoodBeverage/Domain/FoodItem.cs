using BuildingBlocks.Domain;
using Modules.FoodBeverage.Domain.Events;

namespace Modules.FoodBeverage.Domain;

public class FoodItem : AggregateRoot
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }

    public int SoldCount { get; private set; }

    public FoodItem(string name, decimal price)
    {
        Name = name;
        Price = price;
        SoldCount = 0;
    }

    public void Sell()
    {
        SoldCount++;
        AddEvent(new FoodSold(Id, Name));
    }
}
