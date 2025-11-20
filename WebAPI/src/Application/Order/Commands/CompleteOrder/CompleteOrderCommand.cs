using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record CompleteOrderCommand(Guid OrderId) : ICommand;
