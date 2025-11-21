using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.RemoveCustomerAddress;

public sealed record RemoveCustomerAddressCommand(Guid CustomerId, Guid AddressId) : ICommand;
