namespace Domain.Aggregates.Product;

public class ProductImage : BaseEntity<Guid>
{
    public Guid ProductId { get; private set; }
    public string ImageUrl { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ProductImage() { } // For ORMs
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
