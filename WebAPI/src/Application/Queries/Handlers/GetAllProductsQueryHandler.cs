using Application.Queries.DTOs;
using Domain.Repositories;
using LiteBus.Queries.Abstractions;

namespace Application.Queries.Handlers;

public class GetAllProductsQueryHandler(IProductsRepository repository)
    : IQueryHandler<GetAllProductsQuery, List<ProductListItemDto>>
{
    private readonly IProductsRepository _repository = repository;

    public async Task<List<ProductListItemDto>> HandleAsync(
        GetAllProductsQuery query,
        CancellationToken cancellationToken
    )
    {
        var products = await _repository.GetCollectionAsync(cancellationToken);
        return products.Select(p => new ProductListItemDto(p.Id, p.Name)).ToList();
    }
}
