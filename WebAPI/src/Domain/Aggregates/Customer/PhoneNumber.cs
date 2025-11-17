using Domain.Abstractions;

namespace Domain.Aggregates.Customer;

public record PhoneNumber(string CountryCode, string Number) : IValueObject;
