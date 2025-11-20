using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.UpdateCustomerName;

public sealed record UpdateCustomerNameCommand(Guid CustomerId, string Name) : ICommand;
