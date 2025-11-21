using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.MarkOrderOnTheWay;

public class MarkOrderOnTheWayHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<MarkOrderOnTheWayCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        MarkOrderOnTheWayCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.MarkOnTheWay();
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
