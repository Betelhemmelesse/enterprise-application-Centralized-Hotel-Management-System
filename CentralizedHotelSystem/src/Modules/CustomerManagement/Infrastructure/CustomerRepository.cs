using Modules.CustomerManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace Modules.CustomerManagement.Infrastructure;

public interface ICustomerRepository
{
    Task<Customer?> GetByEmailAsync(string email);
}

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _db;
    public CustomerRepository(CustomerDbContext db) => _db = db;

    public async Task<Customer?> GetByEmailAsync(string email) =>
        await _db.Customers.FirstOrDefaultAsync(c => c.Email == email);
}
