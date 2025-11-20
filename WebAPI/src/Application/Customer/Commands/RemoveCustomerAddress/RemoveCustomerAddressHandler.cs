using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.RemoveCustomerAddress;

public class RemoveCustomerAddressHandler(ICustomersRepository customersRepository)
    : ICommandHandler<RemoveCustomerAddressCommand>
{
    public async Task HandleAsync(
        RemoveCustomerAddressCommand message,
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

        customer.RemoveAddress(message.AddressId);
        await customersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
