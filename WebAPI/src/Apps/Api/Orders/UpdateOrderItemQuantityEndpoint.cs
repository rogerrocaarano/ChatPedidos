using Application.Order.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class UpdateOrderItemQuantityEndpoint(ICommandMediator commandMediator)
    : Endpoint<UpdateOrderItemQuantityCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/items/{ItemId}/quantity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateOrderItemQuantityCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
