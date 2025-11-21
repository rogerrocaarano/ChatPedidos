using LiteBus.Queries.Abstractions;

namespace Application.Product.Queries.GetProductsListForTelegram;

public record GetProductListForTelegramQuery : IQuery<List<ProductListItemDto>>;

public record ProductListItemDto(Guid Id, string Name, string Description, decimal Price);
