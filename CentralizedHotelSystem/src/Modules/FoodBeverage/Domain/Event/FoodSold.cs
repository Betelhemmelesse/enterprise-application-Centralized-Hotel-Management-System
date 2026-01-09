using BuildingBlocks.Domain;

namespace Modules.FoodBeverage.Domain.Events;

public class FoodSold : DomainEvent
{
    public Guid FoodId { get; }
    public string FoodName { get; }

    public FoodSold(Guid foodId, string foodName)
    {
        FoodId = foodId;
        FoodName = foodName;
    }
}
