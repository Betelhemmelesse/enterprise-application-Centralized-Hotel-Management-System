using BuildingBlocks.Domain;
using Modules.CustomerManagement.Domain.Events;

namespace Modules.CustomerManagement.Domain;

public class Customer : AggregateRoot
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
        AddEvent(new CustomerRegistered(Id, Name, Email));
    }
}
