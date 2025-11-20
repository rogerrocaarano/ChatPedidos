using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record RemoveOrderItemCommand(Guid OrderId, Guid ItemId) : ICommand;
