using Application.Order.Commands.RemoveOrderItem;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class RemoveOrderItemEndpoint(ICommandMediator commandMediator)
    : Endpoint<RemoveOrderItemCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Delete("/orders/{OrderId}/items/{ItemId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveOrderItemCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
