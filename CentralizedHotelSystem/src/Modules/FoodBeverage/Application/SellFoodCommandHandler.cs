using MediatR;
using Modules.FoodBeverage.Domain;
using Modules.FoodBeverage.Infrastructure;
using BuildingBlocks.Infrastructure;

namespace Modules.FoodBeverage.Application;

public class SellFoodCommandHandler : IRequestHandler<SellFoodCommand, Guid>
{
    private readonly FoodDbContext _db;

    public SellFoodCommandHandler(FoodDbContext db) => _db = db;

    public async Task<Guid> Handle(SellFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _db.FoodItems.FindAsync(new object[] { request.FoodId }, cancellationToken);
        if (food == null) throw new Exception("Food item not found");

        food.Sell();

        foreach (var e in food.Events)
        {
            _db.OutboxMessages.Add(new OutboxMessage
            {
                Type = e.GetType().Name,
                Payload = System.Text.Json.JsonSerializer.Serialize(e)
            });
        }

        await _db.SaveChangesAsync(cancellationToken);
        return food.Id;
    }
}
