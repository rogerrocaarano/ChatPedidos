using Application.Customer.Queries.GetCustomerByTelegramId;
using FastEndpoints;
using LiteBus.Queries.Abstractions;

namespace Api.Customers;

public class GetCustomerByTelegramIdEndpoint(IQueryMediator queryMediator)
    : Endpoint<GetCustomerByTelegramIdQuery, CustomerDto?>
{
    private readonly IQueryMediator _queryMediator = queryMediator;

    public override void Configure()
    {
        Get("/customers/by-telegram-id/{telegramId:long}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetCustomerByTelegramIdQuery req, CancellationToken ct)
    {
        var customer = await _queryMediator.QueryAsync(req, ct);
        if (customer == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(customer, ct);
    }
}
