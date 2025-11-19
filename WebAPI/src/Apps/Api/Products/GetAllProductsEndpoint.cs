using Application.Queries;
using Application.Queries.DTOs;
using FastEndpoints;
using LiteBus.Queries.Abstractions;

namespace Api.Products;

public class GetAllProductsEndpoint(IQueryMediator _queryMediator)
    : Endpoint<EmptyRequest, List<ProductListItemDto>>
{
    private readonly IQueryMediator _queryMediator = _queryMediator;

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var result = await _queryMediator.QueryAsync(new GetAllProductsQuery(), ct);
        await Send.OkAsync(result, ct);
    }
}
