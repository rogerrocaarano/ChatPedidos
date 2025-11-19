using Application.Product.Queries.DTOs;
using LiteBus.Queries.Abstractions;

namespace Application.Product.Queries;

public sealed record GetProductDetailsByIdQuery(Guid Id) : IQuery<ProductDetailsDto>;
