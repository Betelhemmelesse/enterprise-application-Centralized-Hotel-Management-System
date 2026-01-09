using MediatR;
using Modules.CustomerManagement.Domain;
using Modules.CustomerManagement.Infrastructure;
using BuildingBlocks.Infrastructure;

namespace Modules.CustomerManagement.Application;

public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, Guid>
{
    private readonly CustomerDbContext _db;
    public RegisterCustomerHandler(CustomerDbContext db) => _db = db;

    public async Task<Guid> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name, request.Email);
        _db.Customers.Add(customer);

        foreach (var e in customer.Events)
        {
            _db.OutboxMessages.Add(new OutboxMessage
            {
                Type = e.GetType().Name,
                Payload = System.Text.Json.JsonSerializer.Serialize(e)
            });
        }

        await _db.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}
