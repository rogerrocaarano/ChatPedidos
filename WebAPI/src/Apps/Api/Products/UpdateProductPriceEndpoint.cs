using Application.Product.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Products;

public class UpdateProductPriceEndpoint(ICommandMediator commandMediator)
    : Endpoint<UpdateProductPriceCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/products/{ProductId}/price");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductPriceCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
