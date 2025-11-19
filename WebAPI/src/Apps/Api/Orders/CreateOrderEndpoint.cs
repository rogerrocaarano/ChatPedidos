using Application.Order.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Orders;

public class CreateOrderEndpoint(ICommandMediator commandMediator)
    : Endpoint<CreateOrderCommand, Guid>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Post("/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderCommand req, CancellationToken ct)
    {
        var orderId = await _commandMediator.SendAsync(req, ct);
        await Send.OkAsync(orderId, ct);
    }
}
