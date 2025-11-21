using Application.Product.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Products;

public class UpdateProductNameEndpoint(ICommandMediator commandMediator)
    : Endpoint<UpdateProductNameCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/products/{ProductId}/name");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductNameCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
