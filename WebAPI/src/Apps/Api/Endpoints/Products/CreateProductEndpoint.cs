using System;
using Api.Requests.Products;
using Api.Responses.Products;
using Application.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Endpoints.Products;

public class CreateProductEndpoint(ICommandMediator commandMediator)
    : Endpoint<CreateProductRequest, CreateProductResponse>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Post("/products");
        // TODO: Add authorization when needed
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        var command = new CreateProductCommand(req.Name, req.Description, req.Price);
        var productId = await _commandMediator.SendAsync(command, ct);
        await Send.OkAsync(new CreateProductResponse(productId), ct);
    }
}
