using Domain.Repositories;
using Domain.ValueObjects;
using LiteBus.Commands.Abstractions;
using Aggregate = Domain.Aggregates.Customer;

namespace Application.Customer.Commands.Handlers;

public class AddCustomerAddressHandler(ICustomersRepository customersRepository)
    : ICommandHandler<AddCustomerAddressCommand>
{
    public async Task HandleAsync(
        AddCustomerAddressCommand message,
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

        var location = new LocationPoint(message.Latitude, message.Longitude);
        customer.AddAddress(location, message.AddressName);
        await customersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
