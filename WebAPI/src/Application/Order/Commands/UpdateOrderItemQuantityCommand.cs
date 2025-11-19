using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record UpdateOrderItemQuantityCommand(Guid OrderId, Guid ItemId, int Quantity)
    : ICommand;
