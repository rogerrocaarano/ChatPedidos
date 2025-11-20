using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.MarkOrderAsReadyForPickup;

public class MarkOrderAsReadyForPickupHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<MarkOrderAsReadyForPickupCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        MarkOrderAsReadyForPickupCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.MarkAsReadyForPickup();
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
