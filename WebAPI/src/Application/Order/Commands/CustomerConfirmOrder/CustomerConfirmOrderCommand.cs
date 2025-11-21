using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.CustomerConfirmOrder;

public sealed record CustomerConfirmOrderCommand(Guid OrderId) : ICommand;
