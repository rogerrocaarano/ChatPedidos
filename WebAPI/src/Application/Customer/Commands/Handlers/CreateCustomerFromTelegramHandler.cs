using Domain.Aggregates.Customer;
using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.Handlers;

public sealed class CreateCustomerFromTelegramHandler(ICustomersRepository customerRepository)
    : ICommandHandler<CreateCustomerFromTelegramCommand, Guid>
{
    private readonly ICustomersRepository _customerRepository = customerRepository;

    public async Task<Guid> HandleAsync(
        CreateCustomerFromTelegramCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var telegramId = new TelegramId(message.TelegramId);
        var customer = global::Domain.Aggregates.Customer.Customer.CreateFromTelegram(telegramId);
        _customerRepository.Add(customer);
        await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}
