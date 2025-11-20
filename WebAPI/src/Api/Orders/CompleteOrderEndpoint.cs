using Application.Order.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class CompleteOrderEndpoint(ICommandMediator commandMediator)
    : Endpoint<CompleteOrderCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/complete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CompleteOrderCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
