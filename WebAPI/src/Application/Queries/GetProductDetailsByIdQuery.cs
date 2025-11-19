using Application.Queries.DTOs;
using LiteBus.Queries.Abstractions;

namespace Application.Queries;

public sealed record GetProductDetailsByIdQuery(Guid Id) : IQuery<ProductDetailsDto>;
