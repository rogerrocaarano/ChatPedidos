using Application.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Products;

public class CreateProductEndpoint(ICommandMediator commandMediator)
    : Endpoint<CreateProductCommand, Guid>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Post("/products");
        // TODO: Add authorization when needed
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductCommand req, CancellationToken ct)
    {
        var productId = await _commandMediator.SendAsync(req, ct);
        await Send.OkAsync(productId, ct);
    }
}
