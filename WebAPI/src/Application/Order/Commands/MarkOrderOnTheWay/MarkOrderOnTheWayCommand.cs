using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.MarkOrderOnTheWay;

public sealed record MarkOrderOnTheWayCommand(Guid OrderId) : ICommand;
