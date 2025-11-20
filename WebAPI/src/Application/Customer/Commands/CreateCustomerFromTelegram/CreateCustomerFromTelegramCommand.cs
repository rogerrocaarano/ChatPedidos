using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands;

public sealed record CreateCustomerFromTelegramCommand(string Name, long TelegramId)
    : ICommand<Guid> { }
