using Domain.Abstractions;
using Domain.Aggregates.Customer;
using LiteBus.Commands.Abstractions;

namespace Application.Features.CreateCustomerFromTelegram;

public sealed class CreateCustomerFromTelegramHandler(IRepository<Customer> customerRepository)
    : ICommandHandler<CreateCustomerFromTelegramCommand, Customer>
{
    private readonly IRepository<Customer> _customerRepository = customerRepository;

    public Task<Customer> HandleAsync(
        CreateCustomerFromTelegramCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var telegramId = new TelegramId(message.TelegramId);
        var customer = Customer.CreateFromTelegram(telegramId);
        _customerRepository.Add(customer);
        return Task.FromResult(customer);
    }
}
