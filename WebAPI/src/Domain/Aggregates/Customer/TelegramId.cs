using Domain.Abstractions;

namespace Domain.Aggregates.Customer;

public record TelegramId(long Id) : IValueObject;
