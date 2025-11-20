using Domain.ValueObjects;
using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.Handlers;

public class UpdateCustomerAddressHandler(ICustomersRepository customersRepository)
    : ICommandHandler<UpdateCustomerAddressCommand>
{
    public async Task HandleAsync(
        UpdateCustomerAddressCommand message,
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
        customer.UpdateAddress(message.AddressId, location, message.AddressName);
        await customersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
