using Application.Order.Queries.DTOs;
using Domain.Repositories;
using LiteBus.Queries.Abstractions;

namespace Application.Order.Queries.Handlers;

public class GetOrdersByCustomerQueryHandler(IOrdersRepository ordersRepository)
    : IQueryHandler<GetOrdersByCustomerQuery, List<OrderSummaryDto>>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task<List<OrderSummaryDto>> HandleAsync(
        GetOrdersByCustomerQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var orders = await _ordersRepository.GetByCustomerIdAsync(
            query.CustomerId,
            cancellationToken
        );

        return orders
            .Select(order => new OrderSummaryDto(
                order.Id,
                order.CustomerId,
                order.Status,
                order.TotalAmount(),
                order.CreatedAt
            ))
            .ToList();
    }
}
