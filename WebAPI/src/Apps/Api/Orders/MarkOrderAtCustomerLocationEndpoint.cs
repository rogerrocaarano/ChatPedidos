using Application.Order.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class MarkOrderAtCustomerLocationEndpoint(ICommandMediator commandMediator)
    : Endpoint<MarkOrderAtCustomerLocationCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/at-customer-location");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        MarkOrderAtCustomerLocationCommand req,
        CancellationToken ct
    )
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
