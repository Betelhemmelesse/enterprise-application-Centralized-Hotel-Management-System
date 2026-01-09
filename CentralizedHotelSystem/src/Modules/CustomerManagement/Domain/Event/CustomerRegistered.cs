using BuildingBlocks.Domain;

namespace Modules.CustomerManagement.Domain.Events;

public class CustomerRegistered : DomainEvent
{
    public Guid CustomerId { get; }
    public string Name { get; }
    public string Email { get; }

    public CustomerRegistered(Guid id, string name, string email)
    {
        CustomerId = id;
        Name = name;
        Email = email;
    }
}
