using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record CustomerConfirmOrderCommand(Guid OrderId) : ICommand;
