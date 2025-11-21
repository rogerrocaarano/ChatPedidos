using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.MarkOrderAsReadyForPickup;

public sealed record MarkOrderAsReadyForPickupCommand(Guid OrderId) : ICommand;
