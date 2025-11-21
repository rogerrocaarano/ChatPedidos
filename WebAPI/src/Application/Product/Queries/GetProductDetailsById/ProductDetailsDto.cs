namespace Application.Product.Queries.GetProductDetailsById;

public sealed record ProductDetailsDto(Guid Id, string Name, string Description, decimal Price);
