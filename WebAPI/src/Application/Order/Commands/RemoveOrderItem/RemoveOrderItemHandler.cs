using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.RemoveOrderItem;

public class RemoveOrderItemHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<RemoveOrderItemCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        RemoveOrderItemCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.RemoveItem(command.ItemId);
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
