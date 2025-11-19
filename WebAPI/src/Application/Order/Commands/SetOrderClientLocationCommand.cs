using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands;

public sealed record SetOrderClientLocationCommand(Guid OrderId, float Latitude, float Longitude)
    : ICommand;
