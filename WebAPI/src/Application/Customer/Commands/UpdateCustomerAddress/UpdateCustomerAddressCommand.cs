using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands;

public sealed record UpdateCustomerAddressCommand(
    Guid CustomerId,
    Guid AddressId,
    string AddressName,
    float Latitude,
    float Longitude
) : ICommand;
