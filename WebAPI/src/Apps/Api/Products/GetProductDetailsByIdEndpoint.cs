using Application.Product.Queries;
using Application.Product.Queries.DTOs;
using FastEndpoints;
using LiteBus.Queries.Abstractions;

namespace Api.Products;

public class GetProductDetailsByIdEndpoint(IQueryMediator queryMediator)
    : Endpoint<GetProductDetailsByIdQuery, ProductDetailsDto>
{
    private readonly IQueryMediator _queryMediator = queryMediator;

    public override void Configure()
    {
        Get("/products/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductDetailsByIdQuery req, CancellationToken ct)
    {
        // TODO: Handle not found scenario
        var productDetails = await _queryMediator.QueryAsync(req, ct);
        await Send.OkAsync(productDetails, ct);
    }
}
