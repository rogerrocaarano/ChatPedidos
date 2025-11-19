using LiteBus.Commands.Abstractions;

namespace Application.Commands;

public sealed record RemoveProductImageCommand(Guid ProductId, Guid ImageId) : ICommand;
