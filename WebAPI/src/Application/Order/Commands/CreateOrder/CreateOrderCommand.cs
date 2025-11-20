using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record CreateOrderCommand(Guid CustomerId) : ICommand<Guid>;
