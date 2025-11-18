namespace Domain.Aggregates.Common;

public record LocationPoint(float Latitude, float Longitude) : IValueObject;
