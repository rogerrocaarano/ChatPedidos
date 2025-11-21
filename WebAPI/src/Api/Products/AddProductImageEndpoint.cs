using Application.Product.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Products;

public class AddProductImageEndpoint(ICommandMediator commandMediator)
    : Endpoint<AddProductImageCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Post("/products/{ProductId}/images");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddProductImageCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
