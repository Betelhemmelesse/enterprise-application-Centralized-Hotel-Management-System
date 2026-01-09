using Modules.FoodBeverage.Domain;
using Modules.FoodBeverage.Infrastructure;
using System.Threading.Tasks;

namespace Modules.FoodBeverage.Infrastructure
{
    public class DataSeeder
    {
        private readonly FoodDbContext _db;
        public DataSeeder(FoodDbContext db) => _db = db;

        public async Task SeedAsync()
        {
            if (!_db.FoodItems.Any())
            {
                var pizza = new FoodItem("Pizza", 12.5m);
                var burger = new FoodItem("Burger", 8.0m);
                var pasta = new FoodItem("Pasta", 10.0m);
                _db.FoodItems.AddRange(pizza, burger, pasta);
                await _db.SaveChangesAsync();
            }
        }
    }
}
