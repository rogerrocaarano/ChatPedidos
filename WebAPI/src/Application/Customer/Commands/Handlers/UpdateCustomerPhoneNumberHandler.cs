using Domain.Repositories;
using LiteBus.Commands.Abstractions;
using Aggregate = Domain.Aggregates.Customer;

namespace Application.Customer.Commands.Handlers;

public class UpdateCustomerPhoneNumberHandler(ICustomersRepository customersRepository)
    : ICommandHandler<UpdateCustomerPhoneNumberCommand>
{
    public async Task HandleAsync(
        UpdateCustomerPhoneNumberCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var customer = await customersRepository.GetByIdAsync(
            message.CustomerId,
            cancellationToken
        );
        if (customer == null)
        {
            throw new InvalidOperationException("Customer not found");
        }

        var phoneNumber = new Aggregate.PhoneNumber(message.CountryCode, message.Number);
        customer.UpdatePhoneNumber(phoneNumber);
        await customersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
