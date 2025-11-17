using Domain.Aggregates.Customer;
using LiteBus.Commands.Abstractions;

namespace Application.Features.CreateCustomerFromTelegram;

public sealed record CreateCustomerFromTelegramCommand(long TelegramId) : ICommand<Customer> { }
