using Domain.Aggregates.Order;
using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.Handlers;

public class CreateOrderHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<CreateOrderCommand, Guid>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task<Guid> HandleAsync(
        CreateOrderCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = Domain.Aggregates.Order.Order.Create(command.CustomerId);
        _ordersRepository.Add(order);
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}
