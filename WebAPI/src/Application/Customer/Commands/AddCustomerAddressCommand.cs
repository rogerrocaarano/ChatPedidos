using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands;

public sealed record AddCustomerAddressCommand(
    Guid CustomerId,
    string AddressName,
    float Latitude,
    float Longitude
) : ICommand;
