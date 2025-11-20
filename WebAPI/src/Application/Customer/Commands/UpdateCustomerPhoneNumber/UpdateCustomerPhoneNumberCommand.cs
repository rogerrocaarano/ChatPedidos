using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands;

public sealed record UpdateCustomerPhoneNumberCommand(
    Guid CustomerId,
    string CountryCode,
    string Number
) : ICommand;
