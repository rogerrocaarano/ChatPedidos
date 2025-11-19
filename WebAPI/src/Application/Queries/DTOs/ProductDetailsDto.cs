namespace Application.Queries.DTOs;

public sealed record ProductDetailsDto(Guid Id, string Name, string Description, decimal Price);
