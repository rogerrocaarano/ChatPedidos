using Domain.Abstractions;

namespace Domain.Aggregates.Product;

public class ProductImage : BaseEntity<Guid>
{
    public Guid ProductId { get; private set; }
    public string ImageUrl { get; private set; }

    private ProductImage(Guid id, Guid productId, string imageUrl)
        : base(id)
    {
        ProductId = productId;
        ImageUrl = imageUrl;
    }

    public static ProductImage Create(Guid productId, string imageUrl)
    {
        return new ProductImage(id: Guid.NewGuid(), productId: productId, imageUrl: imageUrl);
    }

    public void UpdateUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}
