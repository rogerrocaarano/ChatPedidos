using Domain.Aggregates.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name).IsRequired();
        builder.Property(product => product.Description).IsRequired();
        builder.Property(product => product.Price).IsRequired();
        builder.Property(product => product.IsAvailable).IsRequired();
        builder.Property(product => product.MainImageId);

        // Configure Images as owned entities
        builder.OwnsMany(
            product => product.Images,
            productImage =>
            {
                productImage.WithOwner().HasForeignKey("ProductId");
                productImage.Property(image => image.Id).ValueGeneratedOnAdd();
                productImage.HasKey(image => image.Id);
                productImage.Property(image => image.ProductId).IsRequired();
                productImage.Property(image => image.ImageUrl).IsRequired();
            }
        );
    }
}
