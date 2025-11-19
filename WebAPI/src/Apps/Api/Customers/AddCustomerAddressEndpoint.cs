using Application.Customer.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Customers;

public class AddCustomerAddressEndpoint(ICommandMediator commandMediator)
    : Endpoint<AddCustomerAddressCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Post("/customers/{CustomerId}/addresses");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddCustomerAddressCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
