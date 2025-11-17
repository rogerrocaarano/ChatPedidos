using Domain.Abstractions;

namespace Domain.Aggregates.ProductAggregate;

public class Product : BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; }

    public List<ImageProduct> Images { get; private set; }

    private Product(
        Guid id,
        string name,
        string description,
        decimal price,
        bool isAvailable,
        List<ImageProduct> images
    ) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
        Images = images;
    }

    public static Product Create(string name, string description, decimal price)
    {
        return new Product(
            id: Guid.NewGuid(),
            name: name,
            description: description,
            price: price,
            isAvailable: true,
            images: new List<ImageProduct>()
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

    public void UpdateAvailability(bool isAvailable) => IsAvailable = isAvailable;

    public void UpdateAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }

    public void AddImage(string imageUrl, bool isMain = false)
    {
        if (isMain)
        {
            foreach (var img in Images)
                img.SetAsMain(false);
        }

        Images.Add(ImageProduct.Create(imageUrl, isMain));
    }

    public void RemoveImage(Guid imageId)
    {
        Images.RemoveAll(i => i.Id == imageId);
    }

    public void MarkImageAsMain(Guid imageId)
    {
        foreach (var img in Images)
            img.SetAsMain(img.Id == imageId);
    }




}
