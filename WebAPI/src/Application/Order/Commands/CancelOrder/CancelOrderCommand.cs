using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record CancelOrderCommand(Guid OrderId) : ICommand;
