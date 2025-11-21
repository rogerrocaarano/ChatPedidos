using Application.Order.Queries.GetOrdersByCustomer;
using LiteBus.Queries.Abstractions;

namespace Application.Order.Queries.GetOrdersByCustomer;

public sealed record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<List<OrderSummaryDto>>;
