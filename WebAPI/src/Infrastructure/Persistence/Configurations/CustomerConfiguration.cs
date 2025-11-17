using Domain.Aggregates.Customer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsMany(
            c => c.Addresses,
            a =>
            {
                a.ToTable("Addresses");
                a.WithOwner().HasForeignKey("CustomerId");
                a.HasKey("Id");
            }
        );
    }
}
