using Application.Order.Queries.GetOrdersByCustomer;
using FastEndpoints;
using LiteBus.Queries.Abstractions;

namespace Api.Orders;

public class GetOrdersByCustomerEndpoint(IQueryMediator queryMediator)
    : Endpoint<GetOrdersByCustomerQuery, List<OrderSummaryDto>>
{
    private readonly IQueryMediator _queryMediator = queryMediator;

    public override void Configure()
    {
        Get("/customers/{CustomerId}/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOrdersByCustomerQuery req, CancellationToken ct)
    {
        var result = await _queryMediator.QueryAsync(req, ct);
        await Send.OkAsync(result, ct);
    }
}
