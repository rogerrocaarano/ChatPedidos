using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record MarkOrderAsReadyForPickupCommand(Guid OrderId) : ICommand;
