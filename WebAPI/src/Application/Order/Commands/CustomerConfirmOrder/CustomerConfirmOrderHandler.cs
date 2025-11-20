using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.CustomerConfirmOrder;

public class CustomerConfirmOrderHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<CustomerConfirmOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        CustomerConfirmOrderCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.CustomerConfirm();
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
