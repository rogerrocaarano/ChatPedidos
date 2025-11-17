using System;

namespace Persistence.Configurations;

using System;
using Domain.Aggregates.Order;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder
    )
    {
        builder.HasKey(order => order.Id);

        builder.Property(order => order.CustomerId).IsRequired();
        builder.Property(order => order.Status).HasConversion<string>().IsRequired();
        builder.Property(order => order.PaymentId);
        builder.Property(order => order.RiderId);

        // Configure ClientLocation as owned value object
        builder.OwnsOne(
            order => order.ClientLocation,
            clientLocation =>
            {
                clientLocation.Property(prop => prop.Latitude);
                clientLocation.Property(prop => prop.Longitude);
            }
        );

        // Configure Items as owned entities
        builder.OwnsMany(
            order => order.Items,
            orderItem =>
            {
                orderItem.WithOwner().HasForeignKey("OrderId");
                orderItem.HasKey(item => item.Id);
                orderItem.Property(item => item.ProductId).IsRequired();
                orderItem.Property(item => item.Quantity).IsRequired();
                orderItem.Property(item => item.UnitPrice).IsRequired();
            }
        );
    }
}
