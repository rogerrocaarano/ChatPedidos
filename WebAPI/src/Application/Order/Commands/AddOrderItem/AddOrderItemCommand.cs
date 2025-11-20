using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.AddOrderItem;

public sealed record AddOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    float UnitPrice
) : ICommand;
