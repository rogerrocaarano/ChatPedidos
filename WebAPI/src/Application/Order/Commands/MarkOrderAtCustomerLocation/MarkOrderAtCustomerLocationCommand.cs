using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.MarkOrderAtCustomerLocation;

public sealed record MarkOrderAtCustomerLocationCommand(Guid OrderId) : ICommand;
