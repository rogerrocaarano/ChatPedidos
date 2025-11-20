using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands;

public sealed record SetProductAvailabilityCommand(Guid ProductId, bool IsAvailable) : ICommand;
