using MediatR;

namespace Modules.FoodBeverage.Application;

public record SellFoodCommand(Guid FoodId) : IRequest<Guid>;
