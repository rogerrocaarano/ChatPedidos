using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands;

public sealed record UpdateProductPriceCommand(Guid ProductId, decimal Price) : ICommand;
