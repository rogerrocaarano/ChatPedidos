using LiteBus.Commands.Abstractions;

namespace Application.Commands;

public sealed record CreateCustomerFromTelegramCommand(long TelegramId) : ICommand<Guid> { }
