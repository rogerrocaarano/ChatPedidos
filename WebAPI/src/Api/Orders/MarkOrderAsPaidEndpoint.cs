using Application.Order.Commands.MarkOrderAsPaid;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class MarkOrderAsPaidEndpoint(ICommandMediator commandMediator)
    : Endpoint<MarkOrderAsPaidCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/paid");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MarkOrderAsPaidCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
