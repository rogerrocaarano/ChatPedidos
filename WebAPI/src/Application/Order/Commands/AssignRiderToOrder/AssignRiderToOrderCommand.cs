using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record AssignRiderToOrderCommand(Guid OrderId, Guid RiderId) : ICommand;
