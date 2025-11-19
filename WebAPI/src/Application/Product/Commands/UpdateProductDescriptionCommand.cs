using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands;

public sealed record UpdateProductDescriptionCommand(Guid ProductId, string Description) : ICommand;
