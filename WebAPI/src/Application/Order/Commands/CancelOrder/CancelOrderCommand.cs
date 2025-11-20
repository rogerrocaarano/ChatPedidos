using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.CancelOrder;

public sealed record CancelOrderCommand(Guid OrderId) : ICommand;
