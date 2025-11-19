using Application.Product.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Products;

public class SetProductAvailabilityEndpoint(ICommandMediator commandMediator)
    : Endpoint<SetProductAvailabilityCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/products/{ProductId}/availability");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetProductAvailabilityCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
