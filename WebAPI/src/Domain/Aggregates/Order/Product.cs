using Domain.Abstractions;

namespace Domain.Aggregates.ProductAggregate;

public class Product : BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public Guid? MainImageId { get; private set; }

    public List<ProductImage> Images { get; private set; }

    private Product(
        Guid id,
        string name,
        string description,
        decimal price,
        bool isAvailable,
        List<ProductImage> images,
        Guid? mainImageId
    )
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
        Images = images;
        MainImageId = mainImageId;
    }

    public static Product Create(string name, string description, decimal price)
    {
        return new Product(
            id: Guid.NewGuid(),
            name: name,
            description: description,
            price: price,
            isAvailable: true,
            images: new List<ProductImage>(),
            mainImageId: null
        );
    }

    public void UpdateDetails(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public void UpdateName(string name) => Name = name;

    public void UpdateDescription(string description) => Description = description;

    public void UpdatePrice(decimal price) => Price = price;

    public void SetProductAsAvailable() => IsAvailable = true;

    public void SetProductAsUnavailable() => IsAvailable = false;

    public void AddImage(string imageUrl, bool setAsMain = false)
    {
        var newImage = ProductImage.Create(Id, imageUrl);
        Images.Add(newImage);

        if (setAsMain)
            MainImageId = newImage.Id;
    }

    public void RemoveImage(Guid imageId)
    {
        var img = Images.FirstOrDefault(i => i.Id == imageId);
        if (img == null)
            return;

        Images.Remove(img);

        if (MainImageId == imageId)
            MainImageId = null; // si era la principal, desmarcar
    }

    public void MarkImageAsMain(Guid imageId)
    {
        if (!Images.Any(i => i.Id == imageId))
            throw new InvalidOperationException("Image does not belong to this product");

        MainImageId = imageId;
    }

    public ProductImage? GetMainImage()
    {
        if (MainImageId == null)
            return null;

        return Images.FirstOrDefault(i => i.Id == MainImageId);
    }
}
