using MediatR;

namespace Modules.CustomerManagement.Application;

public record RegisterCustomerCommand(string Name, string Email) : IRequest<Guid>;
