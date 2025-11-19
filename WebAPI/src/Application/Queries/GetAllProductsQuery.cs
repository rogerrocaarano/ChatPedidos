using Application.Queries.DTOs;
using LiteBus.Queries.Abstractions;

namespace Application.Queries;

public record GetAllProductsQuery : IQuery<List<ProductListItemDto>>;
