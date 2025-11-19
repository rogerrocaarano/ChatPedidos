using Application.Product.Queries.DTOs;
using Domain.Repositories;
using LiteBus.Queries.Abstractions;

namespace Application.Product.Queries.Handlers;

public sealed class GetProductDetailsByIdQueryHandler(IProductsRepository productsRepository)
    : IQueryHandler<GetProductDetailsByIdQuery, ProductDetailsDto>
{
    private readonly IProductsRepository _productsRepository = productsRepository;

    public async Task<ProductDetailsDto> HandleAsync(
        GetProductDetailsByIdQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var product =
            await _productsRepository.GetByIdAsync(query.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Product with Id {query.Id} not found.");
        return new ProductDetailsDto(product.Id, product.Name, product.Description, product.Price);
    }
}
