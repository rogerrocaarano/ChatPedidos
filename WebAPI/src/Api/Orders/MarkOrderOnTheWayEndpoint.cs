using Application.Order.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class MarkOrderOnTheWayEndpoint(ICommandMediator commandMediator)
    : Endpoint<MarkOrderOnTheWayCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/on-the-way");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MarkOrderOnTheWayCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
