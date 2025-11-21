using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands;

public sealed record CreateProductCommand(string Name, string Description, decimal Price)
    : ICommand<Guid> { }
