using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record MarkOrderOnTheWayCommand(Guid OrderId) : ICommand;
