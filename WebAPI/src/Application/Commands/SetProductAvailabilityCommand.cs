using LiteBus.Commands.Abstractions;

namespace Application.Commands;

public sealed record SetProductAvailabilityCommand(Guid ProductId, bool IsAvailable) : ICommand;
