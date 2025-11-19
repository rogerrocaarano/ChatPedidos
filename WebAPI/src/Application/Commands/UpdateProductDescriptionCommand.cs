using LiteBus.Commands.Abstractions;

namespace Application.Commands;

public sealed record UpdateProductDescriptionCommand(Guid ProductId, string Description) : ICommand;
