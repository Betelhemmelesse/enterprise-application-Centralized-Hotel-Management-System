using Modules.FoodBeverage.Domain;
using Microsoft.EntityFrameworkCore;

namespace Modules.FoodBeverage.Infrastructure;

public interface IFoodItemRepository
{
    Task<List<FoodItem>> GetTopSellingAsync(int top);
}

public class FoodItemRepository : IFoodItemRepository
{
    private readonly FoodDbContext _db;
    public FoodItemRepository(FoodDbContext db) => _db = db;

    public async Task<List<FoodItem>> GetTopSellingAsync(int top) =>
        await _db.FoodItems.OrderByDescending(f => f.SoldCount).Take(top).ToListAsync();
}
