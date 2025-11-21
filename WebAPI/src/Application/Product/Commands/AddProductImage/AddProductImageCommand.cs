using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands;

public sealed record AddProductImageCommand(Guid ProductId, string ImageUrl, bool SetAsMain)
    : ICommand;
