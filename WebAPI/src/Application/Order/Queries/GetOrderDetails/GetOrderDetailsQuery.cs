using Application.Order.Queries.GetOrderDetails;
using LiteBus.Queries.Abstractions;

namespace Application.Order.Queries.GetOrderDetails;

public sealed record GetOrderDetailsQuery(Guid OrderId) : IQuery<OrderDetailsDto>;
