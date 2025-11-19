using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record MarkOrderAtCustomerLocationCommand(Guid OrderId) : ICommand;
