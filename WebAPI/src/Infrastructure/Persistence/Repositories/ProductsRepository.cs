using Domain.Aggregates.Product;
using Domain.Repositories;

namespace Persistence.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly AppDbContext _context;

    public ProductsRepository(AppDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Product aggregate)
    {
        _context.Products.Add(aggregate);
    }

    public void Remove(Product aggregate)
    {
        _context.Products.Remove(aggregate);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context
            .Products.Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }
}
