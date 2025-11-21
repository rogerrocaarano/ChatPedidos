using Application.Order.Commands.AssignRiderToOrder;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class AssignRiderToOrderEndpoint(ICommandMediator commandMediator)
    : Endpoint<AssignRiderToOrderCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/assign-rider");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AssignRiderToOrderCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
