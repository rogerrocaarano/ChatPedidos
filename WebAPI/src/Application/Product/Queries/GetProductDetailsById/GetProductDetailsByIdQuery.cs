using Application.Product.Queries.GetProductDetailsById;
using LiteBus.Queries.Abstractions;

namespace Application.Product.Queries.GetProductDetailsById;

public sealed record GetProductDetailsByIdQuery(Guid Id) : IQuery<ProductDetailsDto>;
