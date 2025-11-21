using Application.Order.Commands.SetOrderClientLocation;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class SetOrderClientLocationEndpoint(ICommandMediator commandMediator)
    : Endpoint<SetOrderClientLocationCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/location");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetOrderClientLocationCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
