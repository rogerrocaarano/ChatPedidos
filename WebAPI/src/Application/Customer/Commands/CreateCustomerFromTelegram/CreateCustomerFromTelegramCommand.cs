using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.CreateCustomerFromTelegram;

public sealed record CreateCustomerFromTelegramCommand(string Name, long TelegramId)
    : ICommand<Guid> { }
