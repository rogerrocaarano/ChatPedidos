using Domain.Abstractions;

namespace Domain.Aggregates.ProductAggregate;

public class ImageProduct : BaseEntity<Guid>
{
    public string ImageUrl { get; private set; }
    public bool IsMain { get; private set; }

    private ImageProduct(Guid id, string imageUrl, bool isMain)
        : base(id)
    {
        ImageUrl = imageUrl;
        IsMain = isMain;
    }

    public static ImageProduct Create(string imageUrl, bool isMain = false)
    {
        return new ImageProduct(
            id: Guid.NewGuid(),
            imageUrl: imageUrl,
            isMain: isMain
        );
    }

    public void SetAsMain(bool isMain)
    {
        IsMain = isMain;
    }
}
