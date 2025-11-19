using Application.Order.Queries.DTOs;
using LiteBus.Queries.Abstractions;

namespace Application.Order.Queries;

public sealed record GetOrderDetailsQuery(Guid OrderId) : IQuery<OrderDetailsDto>;
