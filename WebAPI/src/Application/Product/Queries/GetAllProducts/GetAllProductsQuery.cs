using Application.Product.Queries.DTOs;
using LiteBus.Queries.Abstractions;

namespace Application.Product.Queries;

public record GetAllProductsQuery : IQuery<List<ProductListDto>>;
