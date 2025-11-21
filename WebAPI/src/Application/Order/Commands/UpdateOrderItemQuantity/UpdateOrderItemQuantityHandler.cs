using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.UpdateOrderItemQuantity;

public class UpdateOrderItemQuantityHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<UpdateOrderItemQuantityCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        UpdateOrderItemQuantityCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.UpdateItemQuantity(command.ItemId, command.Quantity);
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
