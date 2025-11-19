using Application.Product.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Products;

public class RemoveProductImageEndpoint(ICommandMediator commandMediator)
    : Endpoint<RemoveProductImageCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Delete("/products/{ProductId}/images/{ImageId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveProductImageCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
