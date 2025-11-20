using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.MarkOrderAsPaid;

public sealed record MarkOrderAsPaidCommand(Guid OrderId, Guid PaymentId) : ICommand;
