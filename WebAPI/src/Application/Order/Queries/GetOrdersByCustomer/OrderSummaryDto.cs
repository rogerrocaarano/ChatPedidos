using Domain.Aggregates.Order;

namespace Application.Order.Queries.DTOs;

public record OrderSummaryDto(
    Guid Id,
    Guid CustomerId,
    OrderStatus Status,
    float TotalAmount,
    DateTime CreatedAt
);
