using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record MarkOrderAsPaidCommand(Guid OrderId, Guid PaymentId) : ICommand;
