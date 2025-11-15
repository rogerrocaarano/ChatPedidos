using Domain.Abstractions;

namespace Domain.Aggregates.Order;

public record LocationPoint(float Latitude, float Longitude) : IValueObject;
