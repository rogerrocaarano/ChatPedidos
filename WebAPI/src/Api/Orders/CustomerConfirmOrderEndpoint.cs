using Application.Order.Commands.CustomerConfirmOrder;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class CustomerConfirmOrderEndpoint(ICommandMediator commandMediator)
    : Endpoint<CustomerConfirmOrderCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/orders/{OrderId}/confirm");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CustomerConfirmOrderCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
