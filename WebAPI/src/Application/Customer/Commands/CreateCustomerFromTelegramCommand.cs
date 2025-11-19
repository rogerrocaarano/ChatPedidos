using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands;

public sealed record CreateCustomerFromTelegramCommand(long TelegramId) : ICommand<Guid> { }
