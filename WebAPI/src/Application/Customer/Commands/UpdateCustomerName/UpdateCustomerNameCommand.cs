using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands;

public sealed record UpdateCustomerNameCommand(Guid CustomerId, string Name) : ICommand;
