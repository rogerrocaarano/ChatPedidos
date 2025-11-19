using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands;

public sealed record RemoveCustomerAddressCommand(Guid CustomerId, Guid AddressId) : ICommand;
