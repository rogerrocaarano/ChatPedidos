using Application.Order.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class MarkOrderAsReadyForPickupEndpoint(ICommandMediator commandMediator)
    : Endpoint<MarkOrderAsReadyForPickupCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/ready-for-pickup");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        MarkOrderAsReadyForPickupCommand req,
        CancellationToken ct
    )
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
