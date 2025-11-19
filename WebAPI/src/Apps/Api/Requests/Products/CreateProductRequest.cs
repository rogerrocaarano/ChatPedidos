namespace Api.Requests.Products;

public record CreateProductRequest(string Name, string Description, decimal Price);
