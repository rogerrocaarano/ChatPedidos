using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.MarkOrderAsPaid;

public class MarkOrderAsPaidHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<MarkOrderAsPaidCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        MarkOrderAsPaidCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.MarkAsPaid(command.PaymentId);
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
