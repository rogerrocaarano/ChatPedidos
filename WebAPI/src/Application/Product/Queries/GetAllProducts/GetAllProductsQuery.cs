using LiteBus.Queries.Abstractions;

namespace Application.Product.Queries.GetAllProducts;

public record GetAllProductsQuery : IQuery<List<ProductListDto>>;
