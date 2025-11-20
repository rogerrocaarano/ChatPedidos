using Domain.Repositories;
using Domain.ValueObjects;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.Handlers;

public class SetOrderClientLocationHandler(IOrdersRepository ordersRepository)
    : ICommandHandler<SetOrderClientLocationCommand>
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;

    public async Task HandleAsync(
        SetOrderClientLocationCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var order = await _ordersRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        var location = new LocationPoint(command.Latitude, command.Longitude);
        order.SetClientLocation(location);
        await _ordersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
