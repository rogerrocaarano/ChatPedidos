using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.CreateOrder;

public sealed record CreateOrderCommand(Guid CustomerId) : ICommand<Guid>;
