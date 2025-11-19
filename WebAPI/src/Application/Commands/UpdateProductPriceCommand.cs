using LiteBus.Commands.Abstractions;

namespace Application.Commands;

public sealed record UpdateProductPriceCommand(Guid ProductId, decimal Price) : ICommand;
