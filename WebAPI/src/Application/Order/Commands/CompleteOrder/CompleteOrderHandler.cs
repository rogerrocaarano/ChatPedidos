using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.CompleteOrder;

public class CompleteOrderHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<CompleteOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        CompleteOrderCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.CompleteOrder();
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
