using Application.Customer.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Customers;

public class UpdateCustomerNameEndpoint(ICommandMediator commandMediator)
    : Endpoint<UpdateCustomerNameCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/customers/{CustomerId}/name");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateCustomerNameCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}