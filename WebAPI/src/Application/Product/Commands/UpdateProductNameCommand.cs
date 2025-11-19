using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands;

public sealed record UpdateProductNameCommand(Guid ProductId, string Name) : ICommand;
