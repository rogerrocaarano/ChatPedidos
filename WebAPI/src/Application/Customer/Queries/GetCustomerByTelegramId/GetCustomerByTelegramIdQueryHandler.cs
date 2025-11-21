using Domain.Repositories;
using LiteBus.Queries.Abstractions;

namespace Application.Customer.Queries.GetCustomerByTelegramId;

public class GetCustomerByTelegramIdQueryHandler(ICustomersRepository customersRepository)
    : IQueryHandler<GetCustomerByTelegramIdQuery, CustomerDto?>
{
    private readonly ICustomersRepository _customersRepository = customersRepository;

    public async Task<CustomerDto?> HandleAsync(
        GetCustomerByTelegramIdQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var customer = await _customersRepository.GetByTelegramIdAsync(
            query.TelegramId,
            cancellationToken
        );

        if (customer == null)
            return null;

        var phoneNumberDto =
            customer.PhoneNumber != null
                ? new PhoneNumberDto(customer.PhoneNumber.CountryCode, customer.PhoneNumber.Number)
                : null;

        var addresses = customer
            .Addresses.Select(address => new AddressDto(address.Id, address.Location, address.Name))
            .ToList();

        return new CustomerDto(
            customer.Id,
            customer.Name,
            phoneNumberDto,
            customer.TelegramId!,
            addresses
        );
    }
}
