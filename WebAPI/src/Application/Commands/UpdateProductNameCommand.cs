using LiteBus.Commands.Abstractions;

namespace Application.Commands;

public sealed record UpdateProductNameCommand(Guid ProductId, string Name) : ICommand;
