using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.Handlers;

public class CancelOrderHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<CancelOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        CancelOrderCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.CancelOrder();
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
