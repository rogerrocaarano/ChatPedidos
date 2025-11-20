using Domain.Aggregates.Order;

namespace Application.Order.Queries.GetOrderDetails;

public record OrderDetailsDto(
    Guid Id,
    Guid CustomerId,
    List<OrderItemDto> Items,
    LocationPointDto? ClientLocation,
    OrderStatus Status,
    Guid? PaymentId,
    Guid? RiderId,
    float TotalAmount
);

public record OrderItemDto(
    Guid Id,
    Guid ProductId,
    int Quantity,
    float UnitPrice,
    float TotalAmount
);

public record LocationPointDto(float Latitude, float Longitude);
