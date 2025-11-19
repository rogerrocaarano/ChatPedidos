using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands;

public sealed record MarkProductImageAsMainCommand(Guid ProductId, Guid ImageId) : ICommand;
