using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.AssignRiderToOrder;

public sealed record AssignRiderToOrderCommand(Guid OrderId, Guid RiderId) : ICommand;
