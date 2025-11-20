using Application.Order.Queries.GetOrderDetails;
using FastEndpoints;
using LiteBus.Queries.Abstractions;

namespace Api.Orders;

public class GetOrderDetailsEndpoint(IQueryMediator queryMediator)
    : Endpoint<GetOrderDetailsQuery, OrderDetailsDto>
{
    private readonly IQueryMediator _queryMediator = queryMediator;

    public override void Configure()
    {
        Get("/orders/{OrderId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOrderDetailsQuery req, CancellationToken ct)
    {
        var result = await _queryMediator.QueryAsync(req, ct);
        await Send.OkAsync(result, ct);
    }
}
