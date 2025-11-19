using Domain.Aggregates.Order;
using Domain.Repositories;

namespace Persistence.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _context;

    public OrdersRepository(AppDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Order aggregate)
    {
        _context.Orders.Add(aggregate);
    }

    public void Remove(Order aggregate)
    {
        _context.Orders.Remove(aggregate);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, ct);
    }

    public async Task<ICollection<Order>> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Orders.Include(order => order.Items)
            .Where(order => order.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<Order>> GetCollectionAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Orders.Include(order => order.Items).ToListAsync(cancellationToken);
    }
}
