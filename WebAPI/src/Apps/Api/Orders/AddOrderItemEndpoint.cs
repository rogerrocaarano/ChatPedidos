using Application.Order.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class AddOrderItemEndpoint(ICommandMediator commandMediator) : Endpoint<AddOrderItemCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Post("/orders/{OrderId}/items");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddOrderItemCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
