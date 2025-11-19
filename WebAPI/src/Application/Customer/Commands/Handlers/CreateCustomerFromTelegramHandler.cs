using Domain.Repositories;
using LiteBus.Commands.Abstractions;
using Aggregate = Domain.Aggregates.Customer;

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
        var telegramId = new Aggregate.TelegramId(message.TelegramId);
        var customer = Aggregate.Customer.Create(message.Name).WithTelegramId(telegramId);
        _customerRepository.Add(customer);
        await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}
