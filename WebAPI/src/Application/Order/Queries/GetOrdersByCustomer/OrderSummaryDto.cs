using Domain.Aggregates.Order;

namespace Application.Order.Queries.GetOrdersByCustomer;

public record OrderSummaryDto(
    Guid Id,
    Guid CustomerId,
    OrderStatus Status,
    float TotalAmount,
    DateTime CreatedAt
);
