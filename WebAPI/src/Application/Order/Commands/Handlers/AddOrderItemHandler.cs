using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.Handlers;

public class AddOrderItemHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<AddOrderItemCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        AddOrderItemCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.AddItem(command.ProductId, command.Quantity, command.UnitPrice);
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
