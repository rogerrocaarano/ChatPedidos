using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.AssignRiderToOrder;

public class AssignRiderToOrderHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<AssignRiderToOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        AssignRiderToOrderCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.AssignRider(command.RiderId);
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
