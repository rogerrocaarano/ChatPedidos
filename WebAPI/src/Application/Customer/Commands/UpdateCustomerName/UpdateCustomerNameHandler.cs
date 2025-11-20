using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.UpdateCustomerName;

public class UpdateCustomerNameHandler(ICustomersRepository customersRepository)
    : ICommandHandler<UpdateCustomerNameCommand>
{
    public async Task HandleAsync(
        UpdateCustomerNameCommand message,
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

        customer.UpdateName(message.Name);
        await customersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
