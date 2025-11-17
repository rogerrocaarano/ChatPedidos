using Domain.Abstractions;
using Domain.Aggregates.Customer;
using LiteBus.Commands.Abstractions;

namespace Application.Features.CreateCustomerFromTelegram;

public sealed class CreateCustomerFromTelegramHandler(IRepository<Customer> customerRepository)
    : ICommandHandler<CreateCustomerFromTelegramCommand, Guid>
{
    private readonly IRepository<Customer> _customerRepository = customerRepository;

    public async Task<Guid> HandleAsync(
        CreateCustomerFromTelegramCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var telegramId = new TelegramId(message.TelegramId);
        var customer = Customer.CreateFromTelegram(telegramId);
        _customerRepository.Add(customer);
        await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}
