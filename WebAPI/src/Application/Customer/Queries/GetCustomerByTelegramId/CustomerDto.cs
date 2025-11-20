using Domain.ValueObjects;

namespace Application.Customer.Queries.GetCustomerByTelegramId;

public record CustomerDto(
    Guid Id,
    string Name,
    PhoneNumberDto? PhoneNumber,
    TelegramId TelegramId,
    List<AddressDto> Addresses
);

public record PhoneNumberDto(string CountryCode, string Number);

public record AddressDto(
    Guid Id,
    LocationPoint Location,
    string Name
);