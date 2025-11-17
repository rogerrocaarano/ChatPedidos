using Domain.Aggregates.Customer;

namespace Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<Customer> Customers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}
