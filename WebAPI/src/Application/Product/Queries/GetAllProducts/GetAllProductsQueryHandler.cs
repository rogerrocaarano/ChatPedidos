using Application.Product.Queries.DTOs;
using Domain.Repositories;
using LiteBus.Queries.Abstractions;

namespace Application.Product.Queries.Handlers;

public class GetAllProductsQueryHandler(IProductsRepository repository)
    : IQueryHandler<GetAllProductsQuery, List<ProductListDto>>
{
    private readonly IProductsRepository _repository = repository;

    public async Task<List<ProductListDto>> HandleAsync(
        GetAllProductsQuery query,
        CancellationToken cancellationToken
    )
    {
        var products = await _repository.GetCollectionAsync(cancellationToken);
        return products.Select(p => new ProductListDto(p.Id, p.Name)).ToList();
    }
}
