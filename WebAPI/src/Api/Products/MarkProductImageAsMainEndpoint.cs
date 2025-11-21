using Application.Product.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Products;

public class MarkProductImageAsMainEndpoint(ICommandMediator commandMediator)
    : Endpoint<MarkProductImageAsMainCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/products/{ProductId}/images/{ImageId}/main");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MarkProductImageAsMainCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
