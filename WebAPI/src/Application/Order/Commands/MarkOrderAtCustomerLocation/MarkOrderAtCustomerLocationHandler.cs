using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.Handlers;

public class MarkOrderAtCustomerLocationHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<MarkOrderAtCustomerLocationCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        MarkOrderAtCustomerLocationCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.MarkAtCustomerLocation();
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
