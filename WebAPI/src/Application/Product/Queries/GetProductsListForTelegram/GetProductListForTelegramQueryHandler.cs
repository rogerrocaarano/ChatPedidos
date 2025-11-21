using Domain.Repositories;
using LiteBus.Queries.Abstractions;

namespace Application.Product.Queries.GetProductsListForTelegram;

public class GetProductListForTelegramQueryHandler(IProductsRepository productsRepository)
    : IQueryHandler<GetProductListForTelegramQuery, List<ProductListItemDto>>
{
    private readonly IProductsRepository _productsRepository = productsRepository;

    public async Task<List<ProductListItemDto>> HandleAsync(
        GetProductListForTelegramQuery message,
        CancellationToken cancellationToken = default
    )
    {
        var products = await _productsRepository.GetCollectionAsync(
            cancellationToken: cancellationToken
        );

        return products
            .Select(p => new ProductListItemDto(
                Id: p.Id,
                Name: p.Name,
                Price: p.Price,
                Description: p.Description
            ))
            .ToList();
    }
}
