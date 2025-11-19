using LiteBus.Commands.Abstractions;

namespace Application.Commands;

public sealed record MarkProductImageAsMainCommand(Guid ProductId, Guid ImageId) : ICommand;
