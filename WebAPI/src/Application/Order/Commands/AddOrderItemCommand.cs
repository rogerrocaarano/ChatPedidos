using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record AddOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    float UnitPrice
) : ICommand;
