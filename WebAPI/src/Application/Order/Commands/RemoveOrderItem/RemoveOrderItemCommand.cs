using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.RemoveOrderItem;

public sealed record RemoveOrderItemCommand(Guid OrderId, Guid ItemId) : ICommand;
