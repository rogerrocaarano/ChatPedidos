using Domain.Aggregates.Customer;
using Domain.Repositories;

namespace Persistence.Repositories;

public class CustomersRepository : ICustomersRepository
{
    private readonly AppDbContext _context;

    public CustomersRepository(AppDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Customer aggregate)
    {
        _context.Customers.Add(aggregate);
    }

    public void Remove(Customer aggregate)
    {
        _context.Customers.Remove(aggregate);
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context
            .Customers.Include(c => c.Addresses)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<ICollection<Customer>> GetCollectionAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Customers.Include(customer => customer.Addresses)
            .ToListAsync(cancellationToken);
    }
}
