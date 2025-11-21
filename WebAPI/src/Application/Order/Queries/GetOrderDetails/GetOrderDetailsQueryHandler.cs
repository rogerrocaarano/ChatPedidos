using Application.Order.Queries.GetOrderDetails;
using Domain.Repositories;
using LiteBus.Queries.Abstractions;

namespace Application.Order.Queries.GetOrderDetails;

public class GetOrderDetailsQueryHandler(IOrdersRepository ordersRepository)
    : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task<OrderDetailsDto> HandleAsync(
        GetOrderDetailsQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(query.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        var items = order
            .Items.Select(item => new OrderItemDto(
                item.Id,
                item.ProductId,
                item.Quantity,
                item.UnitPrice,
                item.TotalAmount()
            ))
            .ToList();

        var clientLocation =
            order.ClientLocation != null
                ? new LocationPointDto(
                    order.ClientLocation.Latitude,
                    order.ClientLocation.Longitude
                )
                : null;

        return new OrderDetailsDto(
            order.Id,
            order.CustomerId,
            items,
            clientLocation,
            order.Status,
            order.PaymentId,
            order.RiderId,
            order.TotalAmount()
        );
    }
}
