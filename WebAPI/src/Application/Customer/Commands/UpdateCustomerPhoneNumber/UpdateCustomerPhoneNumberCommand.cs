using LiteBus.Commands.Abstractions;

namespace Application.Customer.Commands.UpdateCustomerPhoneNumber;

public sealed record UpdateCustomerPhoneNumberCommand(
    Guid CustomerId,
    string CountryCode,
    string Number
) : ICommand;
