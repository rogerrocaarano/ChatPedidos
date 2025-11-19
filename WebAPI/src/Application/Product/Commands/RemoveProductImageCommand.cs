using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands;

public sealed record RemoveProductImageCommand(Guid ProductId, Guid ImageId) : ICommand;
