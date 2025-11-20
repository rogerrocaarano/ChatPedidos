using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.AddCustomerAddress;

public sealed record AddCustomerAddressCommand(
    Guid CustomerId,
    string AddressName,
    float Latitude,
    float Longitude
) : ICommand;
