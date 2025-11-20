using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.CompleteOrder;

public sealed record CompleteOrderCommand(Guid OrderId) : ICommand;
